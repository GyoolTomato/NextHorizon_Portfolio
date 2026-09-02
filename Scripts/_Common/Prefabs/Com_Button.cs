using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Com_Button : Com_Base
{
    //
    [SerializeField] Button _btn;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _deactiveBG;

    //
    Action _onBtn;


    /// <summary>
    /// 
    /// </summary>
    public void ResetUI()
    {        
        _btn.interactable = true;
        _btn.onClick = null;
        _text.text = string.Empty;
        _deactiveBG.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetActive(bool isActive)
    {
        _btn.interactable = isActive;
        _deactiveBG.SetActive(!isActive);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onBtn"></param>
    public void SetBtn(Action onBtn)
    {
        _onBtn = onBtn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    public void SetText(string text)
    {

        _text.text = text;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        _onBtn?.Invoke();
    }
}
