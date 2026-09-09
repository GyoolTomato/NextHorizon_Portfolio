using Observer;
using UnityEngine;

public class Panel_Missions : Panel_Slots<Com_Missions_Slot>, Observer.IObserver<Observer.MissionClaimEvent>
{
    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        pPanelType = EPanelType.Missions;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        Observer.ObserverTracker<MissionClaimEvent>.Instance.Subscribe(this);

        //
        DeactiveSlots();

        foreach (var item in GameData.Instance.pDataMissions.pMissions)
        {
            var slot = ActivateSlot();
            slot.Init(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Hide()
    {
        //
        Observer.ObserverTracker<MissionClaimEvent>.Instance.Unsubscribe(this);

        //
        base.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEvent(Observer.MissionClaimEvent data)
    {
        //
        Manager_UI.Instance.ShowFlash(()=>
        {
            //
            GameData.Instance.pPlayerInfo.SetPlayerExperience(data.Data.playerExperience);

            //
            Manager_UI.Instance.GetPanel(EPanelType.Main).Refresh();

            //
            foreach (var item in pSlots)
            {
                item.Refresh();
            }
        });       
    }
}
