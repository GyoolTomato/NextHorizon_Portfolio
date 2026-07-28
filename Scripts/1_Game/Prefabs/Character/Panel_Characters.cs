using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Characters : Panel_Slots<Com_Characters_Slot>
{
    //
    public enum EViewMode
    {
        Level,
        Grade,
    }

    //
    [SerializeField] Com_ContentsTitle _title;    

    //
    public EViewMode _viewMode = EViewMode.Level;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.Characters;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        _title.Init(OnBtnClose);

        //
        DeactiveSlots();
        foreach (var item in GameData.Instance.pDataCharacter.pCharacters)
        {
            var slot = ActivateSlot();
            slot.Init(item, _viewMode, OnBtnSlot);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        foreach (var slot in pSlots)
        {
            slot.SetValue(_viewMode);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnBtnClose()
    {
        base.OnBtnClose();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnSlot(Character character)
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.CharacterInfo) as Panel_CharacterInfo;
        panel.Init(character);
    }
}