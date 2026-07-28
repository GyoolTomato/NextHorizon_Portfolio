using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using MEC;

public class Manager_Addressable : Singleton<Manager_Addressable>
{
    //
    public AssetReference SpawnablePrefab;

    //
    Dictionary<EPanelType, GameObject> _dicPanels = null;
    Dictionary<string, TextAsset> _dicTables = null;
    Dictionary<string, Sprite> _dicSprites = null;

    //
    public bool pIsAcceptedDownload { private set; get; }
    public bool pIsInit { private set; get; }
    public long pDownloadedBytes { private set; get; }
    public long pTotalBytes { private set; get; }
    public float pDownloadPercent { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        pIsInit = false;
        pIsAcceptedDownload = false;

        Timing.RunCoroutine(CoInitAddressable());
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator<float> CoInitAddressable()
    {
        //
        Debug.Log("Addressables Init Start");

        //
        var handleInit = Addressables.InitializeAsync(false);
        while (handleInit.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }
        if (handleInit.IsValid() == false)
        {
            Addressables.Release(handleInit);

            Debug.LogError("Addressables Init Valid Failed");

            yield break;
        }
        if (handleInit.Status != AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handleInit);

            Debug.LogError("Addressables Init Failed");

            yield break;
        }

        Addressables.Release(handleInit);

        //
        var handleCheck = Addressables.CheckForCatalogUpdates(false);
        while (handleCheck.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }
        if (handleCheck.IsValid() == false)
        {
            Addressables.Release(handleCheck);

            Debug.LogError("Addressables Check Catalogs Valid Failed");

            yield break;
        }
        if (handleCheck.Status != AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handleCheck);

            Debug.LogError("Addressables Check Catalogs Failed");

            yield break;
        }

        //
        if (handleCheck.Result.Count > 0)
        {
            var handleUpdate = Addressables.UpdateCatalogs(handleCheck.Result, false);
            while (handleUpdate.IsDone == false)
            {
                yield return Timing.WaitForOneFrame;
            }
            if (handleUpdate.IsValid() == false)
            {
                Addressables.Release(handleUpdate);
                Addressables.Release(handleCheck);

                Debug.LogError("Addressables Update Catalogs Valid Failed");

                yield break;
            }
            if (handleUpdate.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handleUpdate);
                Addressables.Release(handleCheck);

                Debug.LogError("Addressables Update Catalogs Failed");

                yield break;
            }

            Addressables.Release(handleUpdate);
        }

        Addressables.Release(handleCheck);

        //
        var handleLoc = Addressables.LoadResourceLocationsAsync("Download");
        while (handleLoc.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }
        if (handleLoc.IsValid() == false)
        {
            Debug.LogError("Addressables Resource Location Valid Failed");

            yield break;
        }
        if (handleLoc.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Addressables Resource Location Failed");

            yield break;
        }

        foreach (var loc in handleLoc.Result)
        {
            Debug.Log($"Key: {loc.PrimaryKey}, Type: {loc.ResourceType}, InternalId: {loc.InternalId}");
        }

        //
        var handleSize = Addressables.GetDownloadSizeAsync(handleLoc.Result);
        while (handleSize.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }
        if (handleSize.IsValid() == false)
        {
            Debug.LogError("Addressables Download Size Valid Failed");

            yield break;
        }
        if (handleSize.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Addressables Download Size Failed");

            yield break;
        }

        pDownloadedBytes = handleSize.Result;

        //
        pIsAcceptedDownload = false;
        var isConfirmMessage = false;
        if (pDownloadedBytes > 0)
        {
            //
            Manager_UI.Instance.ShowMessageBox(
                Manager_UI.Instance.GetTextSystem(9990003), 
                string.Format(Manager_UI.Instance.GetTextSystem(9990004), Manager_UI.Instance.GetFileSize(pDownloadedBytes)), 
                Panel_MessageBox.EType.ConfirmCancel, () =>
            {
                isConfirmMessage = true;
                pIsAcceptedDownload = true;                
            },
            () =>
            {
                isConfirmMessage = true;
                pIsAcceptedDownload = false;                
            });

            //
            while (isConfirmMessage == false)
            {
                yield return Timing.WaitForOneFrame;
            }

            //
            if (pIsAcceptedDownload)
            {
                var handleDownload = Addressables.DownloadDependenciesAsync("Download");
                while (handleDownload.IsDone == false)
                {
                    var downloadStatus = handleDownload.GetDownloadStatus();
                    pDownloadedBytes = downloadStatus.DownloadedBytes;
                    pTotalBytes = downloadStatus.TotalBytes;
                    pDownloadPercent = downloadStatus.Percent * 100f;
                    yield return Timing.WaitForOneFrame;
                }
                if (handleDownload.IsValid() == false)
                {
                    Debug.LogError("Addressables Download Valid Failed");

                    yield break;
                }
                if (handleDownload.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("Addressables Download Failed");

                    yield break;
                }
            }
            else
            {
                LogoScene.ChangeState(ELogoState.Logo);

                yield break;
            }
        }

        var panelHandle = Timing.RunCoroutine(LoadAssets_Panel());
        var tableHandle = Timing.RunCoroutine(LoadAssets_Tables());
        var spriteHandle = Timing.RunCoroutine(LoadAssets_Sprites());

        yield return Timing.WaitUntilDone(panelHandle);
        yield return Timing.WaitUntilDone(tableHandle);
        yield return Timing.WaitUntilDone(spriteHandle);

        pIsInit = true;
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator<float> LoadAssets_Panel()
    {
        //
        _dicPanels ??= new Dictionary<EPanelType, GameObject>();
        _dicPanels.Clear();

        //
        var loadList = new List<(EPanelType type, string key, AsyncOperationHandle<GameObject> handle)>();
        for (EPanelType i = EPanelType.None + 1; i < EPanelType.End; i++)
        {
            //
            if (i == EPanelType.Title || i == EPanelType.MessageBox || i == EPanelType.Group_0 || i == EPanelType.Group_1 || i == EPanelType.Group_2)
            {
                continue;
            }

            //
            var key = $"Panel_{i}";
            loadList.Add((i, key, Addressables.LoadAssetAsync<GameObject>(key)));
        }

        //
        while(true)
        {
            bool isAllDone = true;

            foreach (var item in loadList)
            {
                if (item.handle.IsDone == false)
                {
                    isAllDone = false;
                    break;
                }
            }

            if (isAllDone)
            {
                break;
            }

            yield return Timing.WaitForOneFrame;
        }

        //
        foreach (var item in loadList)
        {
            //
            if (item.handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Addressables Load Resource_Panel Failed. Key : {item.key}");

                //
                foreach (var item2 in loadList)
                {
                    if (item2.handle.IsValid())
                    {
                        Addressables.Release(item2.handle);
                    }
                }

                yield break;
            }
        }

        //
        foreach (var item in loadList)
        {
            if (_dicPanels.ContainsKey(item.type))
            {
                Debug.LogError($"Exist '_dicPanels' Type : {item.type}");
                continue;
            }

            _dicPanels.Add(item.type, item.handle.Result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator<float> LoadAssets_Tables()
    {
        //
        _dicTables ??= new Dictionary<string, TextAsset>();
        _dicTables.Clear();

        //
        var handle = Addressables.LoadResourceLocationsAsync("Tables", typeof(TextAsset));
        while (handle.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }

        foreach (var locator in Addressables.ResourceLocators)
        {
            if (locator.Locate("Tables", typeof(TextAsset), out var locations))
            {
                Debug.Log(
                    $"Locator: {locator.LocatorId}, " +
                    $"Location Count: {locations.Count}"
                );

                foreach (var location in locations)
                {
                    Debug.Log(
                        $"PrimaryKey: {location.PrimaryKey}\n" +
                        $"InternalId: {location.InternalId}\n" +
                        $"ProviderId: {location.ProviderId}"
                    );
                }
            }
        }

        //
        switch (handle.Status)
        {
            case AsyncOperationStatus.Succeeded:
                {
                    //
                    var loadList = new List<(string key, AsyncOperationHandle<TextAsset> handle)>();
                    foreach (var item in handle.Result)
                    {
                        //
                        loadList.Add((item.PrimaryKey, Addressables.LoadAssetAsync<TextAsset>(item.PrimaryKey)));
                    }

                    //
                    while (true)
                    {
                        bool isAllDone = true;

                        foreach (var item in loadList)
                        {
                            if (item.handle.IsDone == false)
                            {
                                isAllDone = false;
                                break;
                            }
                        }

                        if (isAllDone == true)
                        {
                            break;
                        }

                        yield return Timing.WaitForOneFrame;
                    }

                    //
                    foreach (var item in loadList)
                    {
                        //
                        if (item.handle.Status != AsyncOperationStatus.Succeeded)
                        {
                            Debug.LogError($"Addressables Load Resource_Table Failed. Key : {item.key}");

                            //
                            foreach (var item2 in loadList)
                            {
                                if (item2.handle.IsValid())
                                {
                                    Addressables.Release(item2.handle);
                                }
                            }

                            Addressables.Release(handle);

                            yield break;
                        }
                    }

                    //
                    foreach (var item in loadList)
                    {
                        if (_dicTables.ContainsKey(item.key))
                        {
                            Debug.LogError($"Exist '_dicTables' Type : {item.key}");
                            continue;
                        }

                        _dicTables.Add(item.key, item.handle.Result);
                    }
                }
                break;
            default:
                {
                    Addressables.Release(handle);

                    Debug.LogError("Addressables Load Resource_Table Failed");

                    yield break;
                }
        }

        //
        Addressables.Release(handle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator<float> LoadAssets_Sprites()
    {
        //
        _dicSprites ??= new Dictionary<string, Sprite>();
        _dicSprites.Clear();

        //
        var handle = Addressables.LoadResourceLocationsAsync("Sprites", typeof(Sprite));
        while (handle.IsDone == false)
        {
            yield return Timing.WaitForOneFrame;
        }

        //
        switch (handle.Status)
        {
            case AsyncOperationStatus.Succeeded:
                {
                    //
                    var loadList = new List<(string key, AsyncOperationHandle<Sprite> handle)>();
                    foreach (var item in handle.Result)
                    {
                        //
                        var name = Path.GetFileNameWithoutExtension(item.PrimaryKey);

                        loadList.Add((name, Addressables.LoadAssetAsync<Sprite>(item.PrimaryKey)));
                    }

                    //
                    while (true)
                    {
                        var isAllDone = true;

                        foreach (var item in loadList)
                        {
                            if (item.handle.IsDone == false)
                            {
                                isAllDone = false;
                                break;
                            }
                        }

                        if (isAllDone == true)
                        {
                            break;
                        }

                        yield return Timing.WaitForOneFrame;
                    }

                    //
                    foreach (var item in loadList)
                    {
                        //
                        if (item.handle.Status != AsyncOperationStatus.Succeeded)
                        {
                            Debug.LogError($"Addressables Load Resource_Sprite Failed. Key : {item.key}");

                            //
                            foreach (var item2 in loadList)
                            {
                                if (item2.handle.IsValid())
                                {
                                    Addressables.Release(item2.handle);
                                }
                            }

                            Addressables.Release(handle);

                            yield break;
                        }
                    }

                    //
                    foreach (var item in loadList)
                    {
                        if (_dicSprites.ContainsKey(item.key))
                        {
                            Debug.LogError($"Exist '_dicSprites' Type : {item.key}");
                            continue;
                        }

                        _dicSprites.Add(item.key, item.handle.Result);
                    }
                }
                break;
            default:
                {
                    Addressables.Release(handle);

                    Debug.LogError("Addressables Load Resource_Sprites Failed");
                    
                    yield break;
                }
        }

        //
        Addressables.Release(handle);
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreatePrefab()
    {
        List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        AsyncOperationHandle<GameObject> handle = SpawnablePrefab.InstantiateAsync();
        handles.Add(handle);
    }

    /// <summary>
    /// 
    /// </summary>
    public void AssetDestruct(GameObject gameObject)
    {
        //
        Addressables.ReleaseInstance(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    public GameObject GetPanel(EPanelType panelType)
    {
        //
        if (_dicPanels.ContainsKey(panelType) == false)
            return null;
        //
        return _dicPanels[panelType];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableKey"></param>
    /// <returns></returns>
    public TextAsset GetTable(string tableKey)
    {
        //
        if (_dicTables.ContainsKey(tableKey) == false)
            return null;

        //
        return _dicTables[tableKey];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spriteKey"></param>
    /// <returns></returns>
    public Sprite GetSprite(string spriteKey)
    {
        //
        if (_dicSprites.ContainsKey(spriteKey) == false)
            return null;

        //
        return _dicSprites[spriteKey];
    }
}
