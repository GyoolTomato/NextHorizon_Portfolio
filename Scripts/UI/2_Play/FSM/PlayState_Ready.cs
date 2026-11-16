using System;
using System.Collections.Generic;
using System.Text;

public class PlayState_Ready : PlayState
{
    //
    public PlayState_Ready(EPlayState state) : base(state)
    {
    }

    //
    override public void Enter()
    {
        Manager_Game.SetGameSpeed(1f);

        Manager_Playing.Instance.Init();

        GameData.Instance.pDataInGame.pRemainTime = Convert.ToSingle(_100_CommonValues.GetItem(1000012).value_int);

        GameData.Instance.pDataInGame.SpawnPlayer();

        Manager_Playing.Instance.SetPlayerCam();        

        var panel = Manager_UI.Instance.ShowPanel(EPanelType.BattleController) as Panel_BattleController;
        panel.Init();

        PlayScene.ChangeState(EPlayState.Playing);
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
