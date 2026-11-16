using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Stage : GameState
{
    //
    public GameState_Stage(EGameState state) : base(state)
    {
    }

    //
    override public void Enter()
    {
        Manager_Game.ChangeScene(ESceneType.Play);
    }

    //
    override public void Exit()
    {
        
    }

    //
    override public void Update()
    {
        
    }
}
