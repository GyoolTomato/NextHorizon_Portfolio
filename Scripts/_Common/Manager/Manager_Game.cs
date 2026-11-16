using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using MEC;

public class Manager_Game : MonoBehaviour
{
    //
    double _tckSecUpdateTime = 0d;

    static ESceneType _nowScene;

    static public float pGameDeltaTime { private set; get; }
    static public float pGameSpeed { private set; get; }

    static public bool pIsStopGameTimer { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
    private Manager_Game()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public static void ChangeScene(ESceneType sceneType)
    {
        //
        switch (_nowScene)
        {
            case ESceneType.Logo: LogoScene.Release(); break;
            case ESceneType.Game: GameScene.Release(); break;
            case ESceneType.Play: PlayScene.Release(); break;
        }

        _nowScene = sceneType;

        Timing.RunCoroutine(CoSceneChange(_nowScene), Segment.Update);
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
            case ESceneType.Play:
                sceneName = "2_Play";
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
            case ESceneType.Play:
                PlayScene.Init();
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
        //
        Manager_UI.Instance.UpdateTicks();

        //
        if (Time.realtimeSinceStartup >= _tckSecUpdateTime + 1f)
        {
            //
            _tckSecUpdateTime = Time.realtimeSinceStartup;

            //
            Manager_UI.Instance.UpdateTicks_Sec();
        }

        //
        pGameDeltaTime = pIsStopGameTimer ? 0f : Time.deltaTime * pGameSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateDatas()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public static void StartGameTimer()
    {
        pIsStopGameTimer = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void StopGameTimer()
    {
        pIsStopGameTimer = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetGameSpeed(float speed)
    {
        pGameSpeed = speed;
    }
}
