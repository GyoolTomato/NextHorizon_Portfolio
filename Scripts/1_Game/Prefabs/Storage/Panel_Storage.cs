using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Storage : Panel_Slots<Com_Item_Slot>
{
    //
    [SerializeField] Com_ContentsTitle _title;

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.Storage;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        _title.Init(OnBtnClose);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnBtnClose()
    {
        base.OnBtnClose();
    }
}
