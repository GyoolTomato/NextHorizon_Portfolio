using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Com_StageSelect_StageDetail : Com_Slots<Com_Reward_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _desc;
    [SerializeField] Com_StageSelect_StageDetail_MissionObjective_Slot _star0;
    [SerializeField] Com_StageSelect_StageDetail_MissionObjective_Slot _star1;
    [SerializeField] Com_StageSelect_StageDetail_MissionObjective_Slot _star2;
    [SerializeField] Com_StageSelect_StageDetail_Rewards _comRewards;

    //
    DataStage _data;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void Init(DataStage data)
    {
        //
        _data = data;

        //
        _title.text = Manager_UI.Instance.GetTextStage(_data.pTableInfo.title);
        _desc.text = Manager_UI.Instance.GetTextStage(_data.pTableInfo.desc);

        //
        _star0.Init(data.pIsCond_1, Manager_UI.Instance.GetTextCommon(9000087));
        _star1.Init(data.pIsCond_2, string.Format(Manager_UI.Instance.GetTextCommon(9000088), _100_CommonValues.GetItem(1000013).value_int));
        _star2.Init(data.pIsCond_3, string.Format(Manager_UI.Instance.GetTextCommon(9000088), _100_CommonValues.GetItem(1000014).value_int));

        //
        DeactiveSlots();

        //
        _comRewards.Init(data);
    }
}
