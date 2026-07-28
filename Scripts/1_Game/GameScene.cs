using Data;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class GameScene
{
    //
    static GameState_Main _stateMain = null;
    static GameState_Stage _stateStage = null;

    static SceneState _curState = null;

    //
    static CoroutineHandle _coroutineHandle;


    //
    static public void Init()
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        //
        _stateMain = new GameState_Main(EGameState.Main);
        _stateStage = new GameState_Stage(EGameState.Stage);

        //
        _curState = null;

        //
        TableDataLoader.Instance.Init();

        GameData.Instance               .Init();
        GameData.Instance.pDataCharacter.Init();
        GameData.Instance.pDataGacha    .Init();
        GameData.Instance.pDataMissions .Init();
        GameData.Instance.pDataShop     .Init();

        //        
        Manager_UI.Instance.Init();
        Manager_Table.Instance.Init();

        //
        ChangeState(EGameState.Main);
    }

    //
    static public void Release()
    {
        //
        _curState?.Exit();

        //
        Timing.KillCoroutines(_coroutineHandle);
    }

    //
    static public void ChangeState(EGameState gameState)
    {
        //
        Timing.KillCoroutines(_coroutineHandle);

        _curState?.Exit();

        //
        switch (gameState)
        {
            case EGameState.Main: _curState = _stateMain; break;
            case EGameState.Stage: _curState = _stateStage; break;
        }

        //
        _curState.Enter();

        _coroutineHandle = Timing.RunCoroutine(Update());
    }

    //
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
