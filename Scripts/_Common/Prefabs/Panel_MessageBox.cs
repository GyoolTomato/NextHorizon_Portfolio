using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_MessageBox : Panel_Base
{
    //
    public enum  EType
    {
        None,
        OK,
        ConfirmCancel,       
    }

    //
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _message;
    [SerializeField] Button _btnConfirm;
    [SerializeField] Button _btnCancel;

    //
    Action _onConfirm;
    Action _onCancel;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.MessageBox;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(string title, string message, EType type, Action onConfirm, Action onCancel)
    {
        _title.text = title;
        _message.text = message;

        _btnConfirm.gameObject.SetActive(type == EType.ConfirmCancel || type == EType.OK);
        _btnCancel.gameObject.SetActive(type == EType.ConfirmCancel);

        _onConfirm = onConfirm;
        _onCancel = onCancel;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnConfirm()
    {
        _onConfirm?.Invoke();

        Manager_UI.Instance.HidePanel(EPanelType.MessageBox);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnCancel()
    {
        _onCancel?.Invoke();

        Manager_UI.Instance.HidePanel(EPanelType.MessageBox);
    }
}
