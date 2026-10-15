using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterInfo : Panel_Base
{
    //
    [SerializeField] Com_ContentsTitle _comTitle;
    [SerializeField] TextMeshProUGUI   _name;
    [SerializeField] TextMeshProUGUI   _level;
    [SerializeField] Image             _characterImage;
    [SerializeField] Com_Button        _btnLevelUp;
    
    [SerializeField] Com_CharacterInfo_Info_Stats   _comStats;
    [SerializeField] Com_CharacterInfo_Info_Skills  _comSkills;

    [SerializeField] Com_EquipmentSelct_Slot _comWeaponSlot = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Hat = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Clothes = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Gloves = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Shoes = null;

    //
    DataCharacter _data = null;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.CharacterInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(DataCharacter character)
    {
        //
        _comTitle.Init(OnBtnClose);

        //
        _data = character;
        _btnLevelUp.SetActive(true);
        _btnLevelUp.SetBtn(OnBtnLevelUp);

        //
        Refresh();

        ServerAPI.Instance.Send_StageClear(7000001, 0.1f, (isSuccess)=>
        {

        },
        (error)=>
        {

        });
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        _name.text = Manager_UI.Instance.GetTextCharacter(_data.pTableInfo.name);
        _characterImage.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.FullBody, _data.pTableInfo.model);
        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), _data.pLevel);        
        _btnLevelUp.gameObject.SetActive(!Manager_Character.Instance.IsMaxLevel(_data.pLevel));
        _comStats.Init(_data);
        _comSkills.Init(_data);

        //
        _comWeaponSlot.Init(_data.pTableInfo.key, EEquipmentType.Weapon, OnHide_WearEquipment);
        _comArmorSlot_Hat.Init(_data.pTableInfo.key, EEquipmentType.Hat, OnHide_WearEquipment);
        _comArmorSlot_Clothes.Init(_data.pTableInfo.key, EEquipmentType.Clothes, OnHide_WearEquipment);
        _comArmorSlot_Gloves.Init(_data.pTableInfo.key, EEquipmentType.Gloves, OnHide_WearEquipment);
        _comArmorSlot_Shoes.Init(_data.pTableInfo.key, EEquipmentType.Shoes, OnHide_WearEquipment);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnHide_WearEquipment()
    {
        Refresh();
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
    void OnBtnLevelUp()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.LevelUp) as Panel_LevelUp;
        panel.Init(_data, ()=>
        {
            Refresh();
        });
    }
}