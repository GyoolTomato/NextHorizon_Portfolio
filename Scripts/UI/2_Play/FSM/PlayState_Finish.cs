using Observer;
using System;
using System.Collections.Generic;
using System.Text;

public class PlayState_Finish : PlayState, Observer.IObserver<StageClearEvent>
{
    //
    bool _isDone = false;


    //
    public PlayState_Finish(EPlayState state) : base(state)
    {
    }

    //
    public void OnEvent(StageClearEvent msg)
    {
        //
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.BattleResult) as Panel_BattleResult;
        panel.Init(msg.Data.weapons, msg.Data.armors, msg.Data.rewards);        

        //
        Manager_UI.Instance.HideTouchLock(true);
    }

    //
    override public void Enter()
    {
        //
        Observer.ObserverTracker<StageClearEvent>.Instance.Subscribe(this);

        //
        Manager_UI.Instance.ShowTouchLock(true);

        //
        var dataInGame = GameData.Instance.pDataInGame;

        //
        foreach (var item in dataInGame.pAllies)
        {
            item.pComAnim.StopAnim();
        }

        //
        foreach (var item in dataInGame.pEnemies)
        {
            item.pComAnim.StopAnim();
        }

        //
        foreach (var item in dataInGame.pNeuts)
        {
            item.pComAnim.StopAnim();
        }
                
        //
        if (Manager_Playing.Instance.pBattleResult > 0)
        {
            ServerAPI.Instance.Send_StageClear(dataInGame.pStageInfo.pTableInfo.key, Manager_Playing.Instance.pBattleResult_RemainTime, (result) =>
            {

            }, (error) =>
            {

            });
        }
        else
        {
            //
            var panel = Manager_UI.Instance.ShowPanel(EPanelType.BattleResult) as Panel_BattleResult;
            panel.Init(null, null, null);

            //
            Manager_UI.Instance.HideTouchLock(true);
        }
    }

    //
    override public void Exit()
    {
        Observer.ObserverTracker<StageClearEvent>.Instance.Unsubscribe(this);
    }

    //
    override public void Update()
    {

    }
}
