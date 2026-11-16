using System;
using TMPro;
using UnityEngine;

public class Com_StageSelect_StageDetail_MissionObjective_Slot : Com_Base
{
    //
    [SerializeField] GameObject _check;
    [SerializeField] TextMeshProUGUI _text;


    /// <summary>
    /// 
    /// </summary>
    public void Init(bool isClear, string text)
    {
        _check.SetActive(isClear);
        _text.text = text;
    }
}
