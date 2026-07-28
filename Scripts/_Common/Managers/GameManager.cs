using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using MEC;

public class GameManager : MonoBehaviour
{
    //
    double _tckSecUpdateTime = 0d;


    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        GlobalData.Instance.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        LogoScene.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ChangeLogoScene()
    {
        Timing.RunCoroutine(CoSceneChange(ESceneType.Logo), Segment.Update);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ChangeGameScene()
    {
        Timing.RunCoroutine(CoSceneChange(ESceneType.Game), Segment.Update);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static IEnumerator<float> CoSceneChange(ESceneType sceneType)
    {
        string sceneName = string.Empty;
        switch (sceneType)
        {
            case ESceneType.Logo:
                sceneName = "0_Logo";
                break;
            case ESceneType.Game:
                sceneName = "1_Game";
                break;
        }

        var handle = SceneManager.LoadSceneAsync(sceneName);
        while (handle.isDone == false)
        {
            yield return 0f;
        }

        switch (sceneType)
        {
            case ESceneType.Logo:
                LogoScene.Init();
                break;
            case ESceneType.Game:
                GameScene.Init();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        Manager_UI.Instance.UpdateTicks();

        if (Time.realtimeSinceStartup >= _tckSecUpdateTime + 1f)
        {
            //
            _tckSecUpdateTime = Time.realtimeSinceStartup;

            //
            Manager_UI.Instance.UpdateTicks_Sec();
        }
    }

    void UpdateDatas()
    {

    }
}
