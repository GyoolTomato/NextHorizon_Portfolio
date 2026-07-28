using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_CharacterInfo_Info_Stats_Slot : Com_Base
{
    //
    [SerializeField] TextMeshProUGUI _title = null;
    [SerializeField] TextMeshProUGUI _value = null;    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="value"></param>
    /// <param name="isPercent"></param>
    public void Init(ECharacterStats characterStats, double value)
    {
        //
        var isPercent = Manager_Character.Instance.IsPercentStatus(characterStats);

        //
        _title.text = Manager_UI.Instance.GetTextCharacterStats(characterStats);
        _value.text = $"{Math.Truncate(value * 100d * (isPercent ? 100d : 1d)) / 100d}";
    }
}
