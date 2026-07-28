using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LogoScene
{
    //
    static LogoState_Logo _stateLogo = null;
    static LogoState_Download _stateDownload = null;
    static LogoState_LogIn _stateLogIn = null;

    static LogoState _curState = null;

    //
    static CoroutineHandle _coroutineHandle;

    //
    public static LogoState_Logo pStateLogo => _stateLogo;
    public static LogoState_Download pStateDownload => _stateDownload;
    public static LogoState_LogIn pStateLogIn => _stateLogIn;
    public static LogoState pCurState => _curState;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoScene"></param>
    public static void Init()
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        //
        _stateLogo = new LogoState_Logo(ELogoState.Logo);
        _stateDownload = new LogoState_Download(ELogoState.Download);
        _stateLogIn = new LogoState_LogIn(ELogoState.LogIn);

        //
        _curState = null;

        //
        Manager_UI.Instance.Init();
        Manager_Resources.Instance.Init();

        //
        ChangeState(ELogoState.Logo);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Release()
    {
        //
        _curState?.Exit();

        //
        Timing.KillCoroutines(_coroutineHandle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoState"></param>
    public static void ChangeState(ELogoState logoState)
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        _curState?.Exit();

        //
        switch (logoState)
        {
            case ELogoState.Logo: _curState = _stateLogo; break;
            case ELogoState.Download: _curState = _stateDownload; break;
            case ELogoState.LogIn: _curState = _stateLogIn; break;
        }

        //
        _curState.Enter();

        _coroutineHandle = Timing.RunCoroutine(Update());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static IEnumerator<float> Update()
    {
        while (true)
        {
            //
            _curState?.Update();

            //
            yield return 0f;
        }
    }
}
