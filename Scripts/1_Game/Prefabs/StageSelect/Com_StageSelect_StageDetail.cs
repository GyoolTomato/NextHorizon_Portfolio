using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Com_StageSelect_StageDetail : Com_Slots<Com_Reward_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _desc;
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
        DeactiveSlots();

        //
        _comRewards.Init(data);
    }
}
