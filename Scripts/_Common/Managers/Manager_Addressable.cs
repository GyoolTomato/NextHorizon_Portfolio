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

            Debug.LogError($"Addressables 초기화 핸들이 유효하지 않음. 상태: {handleInit.Status}, 오류: {handleInit.OperationException}");

            yield break;
        }
        if (handleInit.Status != AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handleInit);

            Debug.LogError($"Addressables 초기화 실패. 상태: {handleInit.Status}, 오류: {handleInit.OperationException}");

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

            Debug.LogError($"Addressables 카탈로그 확인 핸들이 유효하지 않음. 상태: {handleCheck.Status}, 오류: {handleCheck.OperationException}");

            yield break;
        }
        if (handleCheck.Status != AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handleCheck);

            Debug.LogError($"Addressables 카탈로그 확인 실패. 상태: {handleCheck.Status}, 오류: {handleCheck.OperationException}");

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

                Debug.LogError($"Addressables 카탈로그 업데이트 핸들이 유효하지 않음. 상태: {handleUpdate.Status}, 오류: {handleUpdate.OperationException}");

                yield break;
            }
            if (handleUpdate.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handleUpdate);
                Addressables.Release(handleCheck);

                Debug.LogError($"Addressables 카탈로그 업데이트 실패. 상태: {handleUpdate.Status}, 오류: {handleUpdate.OperationException}");

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
            Debug.LogError($"Addressables 리소스 위치 핸들이 유효하지 않음. 상태: {handleLoc.Status}, 오류: {handleLoc.OperationException}");

            yield break;
        }
        if (handleLoc.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Addressables 리소스 위치 조회 실패. 상태: {handleLoc.Status}, 오류: {handleLoc.OperationException}");

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
            Debug.LogError($"Addressables 다운로드 용량 핸들이 유효하지 않음. 상태: {handleSize.Status}, 오류: {handleSize.OperationException}");

            yield break;
        }
        if (handleSize.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Addressables 다운로드 용량 조회 실패. 상태: {handleSize.Status}, 오류: {handleSize.OperationException}");

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
                    Debug.LogError($"Addressables 다운로드 핸들이 유효하지 않음. 상태: {handleDownload.Status}, 오류: {handleDownload.OperationException}");

                    yield break;
                }
                if (handleDownload.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Addressables 에셋 다운로드 실패. 상태: {handleDownload.Status}, 오류: {handleDownload.OperationException}");

                    yield break;
                }
            }
            else
            {
                LogoScene.ChangeState(ELogoState.Logo);

                yield break;
            }
        }

        // MEC invalidates a CoroutineHandle as soon as its coroutine finishes.
        // Starting all three first allowed a later coroutine to finish while an
        // earlier handle was being awaited, making WaitUntilDone assert.
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAssets_Panel()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAssets_Tables()));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAssets_Sprites()));

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
                Debug.LogError($"Addressables 패널 에셋 로드 실패. Key: {item.key}, 상태: {item.handle.Status}, 오류: {item.handle.OperationException}");

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
                            Debug.LogError($"Addressables 테이블 에셋 로드 실패. Key: {item.key}, 상태: {item.handle.Status}, 오류: {item.handle.OperationException}");

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

                    Debug.LogError($"Addressables 테이블 리소스 위치 조회 실패. 상태: {handle.Status}, 오류: {handle.OperationException}");

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
                            Debug.LogError($"Addressables 스프라이트 에셋 로드 실패. Key: {item.key}, 상태: {item.handle.Status}, 오류: {item.handle.OperationException}");

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

                    Debug.LogError($"Addressables 스프라이트 리소스 위치 조회 실패. 상태: {handle.Status}, 오류: {handle.OperationException}");
                    
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
