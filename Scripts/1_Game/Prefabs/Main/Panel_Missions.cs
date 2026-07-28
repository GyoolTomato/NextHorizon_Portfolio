using UnityEngine;

public class Panel_Missions : Panel_Slots<Com_Missions_Slot>
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
        DeactiveSlots();

        foreach (var item in GameData.Instance.pDataMissions.pMissions)
        {
            var slot = ActivateSlot();
            slot.Init(item);
        }
    }
}
