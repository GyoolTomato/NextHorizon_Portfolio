using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_ContentsTitle : Com_Base
{
    //
    [SerializeField] TextMeshProUGUI _title;

    //
    Action _onBtn;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="onBtn"></param>
    public void Init(Action onBtn)
    {
        _onBtn = onBtn;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        _onBtn?.Invoke();
    }
}
