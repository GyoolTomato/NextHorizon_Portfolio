using MEC;
using System;
using System.Collections.Generic;
using System.Text;


public static class PlayScene
{
    //
    static PlayState_Ready _stateReady;
    static PlayState_Playing _statePlaying;
    static PlayState_Pause _statePause;
    static PlayState_Finish _stateFinish;

    static PlayState _curState;
    static PlayState _pauseBeforeState;

    //
    static CoroutineHandle _coroutineHandle;

    //
    public static PlayState_Ready pStateReady => _stateReady;
    public static PlayState_Playing pStatePlaying => _statePlaying;
    public static PlayState_Pause pStatePause => _statePause;
    public static PlayState_Finish pStateFinish => _stateFinish;
    public static PlayState pCurState => _curState;


    /// <summary>
    /// 
    /// </summary>
    public static void Init()
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        //
        _stateReady = new PlayState_Ready(EPlayState.Ready);
        _statePlaying = new PlayState_Playing(EPlayState.Playing);
        _statePause = new PlayState_Pause(EPlayState.Pause);
        _stateFinish = new PlayState_Finish(EPlayState.Finish);

        //
        _curState = null;

        //
        Manager_UI.Instance.Init();
        Manager_Resources.Instance.Init();

        //
        ChangeState(EPlayState.Ready);
    }

    /// <summary>
    /// 
    /// </summary>
    static public void Release()
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
    public static void ChangeState(EPlayState state)
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        _curState?.Exit();

        //
        switch (state)
        {
            case EPlayState.Ready: _curState = _stateReady; break;
            case EPlayState.Pause: _curState = _statePause; break;
            case EPlayState.Playing: _curState = _statePlaying; break;
            case EPlayState.Finish: _curState = _stateFinish; break;
        }

        //
        var enteredState = _curState;
        enteredState.Enter();

        //
        if (_curState != enteredState)
            return;

        _coroutineHandle = Timing.RunCoroutine(Update(), Segment.Update);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Pause(bool isActive)
    {
        if (isActive)
        {
            Manager_Game.StopGameTimer();
        }
        else
        {
            Manager_Game.StartGameTimer();
        }
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
