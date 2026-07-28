using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Panel_Base : MonoBehaviour
{
    //
    bool _isShow = false;

    //
    public EPanelType pPanelType {protected set; get; } = EPanelType.None;
    public bool pIsShow
    {
        set
        {
            _isShow = value;
            gameObject.SetActive(value);
        }
        get
        {
            return _isShow;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    protected virtual void Awake()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void OnShowPanel()
    {
        Tick();
        Tick_Sec();
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void OnHidePanel()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void Show()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Refresh()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void Hide()
    {
        Manager_UI.Instance.HidePanel(pPanelType);
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void OnBtnClose()
    {
        Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Tick()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Tick_Sec()
    {

    }
}
