using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Main : GameState
{
    //
    public GameState_Main(EGameState state) : base(state)
    {
    }

    //
    override public void Enter()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Main) as Panel_Main;
        panel.Init();
        
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