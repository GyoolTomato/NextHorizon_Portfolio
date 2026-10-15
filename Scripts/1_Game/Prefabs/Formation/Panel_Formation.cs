using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using static Panel_Characters;
using TMPro;

public class Panel_Formation : Panel_Slots<Com_Characters_Slot>
{
    //
    [SerializeField] Com_ContentsTitle _title;

    //
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] Image _characterImage;

    //
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotHP = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotATK = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotDEF = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotAvoid = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotFocus = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotAtkSpd = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotSpeed = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotCriRate = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotCriDmg = null;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotActive = null;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotPassive_0 = null;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotPassive_1 = null;

    //
    [SerializeField] Com_EquipmentSelct_Slot _comWeaponSlot = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Hat = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Clothes = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Gloves = null;
    [SerializeField] Com_EquipmentSelct_Slot _comArmorSlot_Shoes = null;

    //
    EViewMode _viewMode = EViewMode.Level;
    DataCharacter _data = null;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.Formation;
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

        //
        if (pSlots.Count >= 1)
            OnBtnSlot(pSlots[0]);
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

        //
        _name.text = Manager_UI.Instance.GetTextCharacter(_data.pTableInfo.name);
        _characterImage.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.FullBody, _data.pTableInfo.model);
        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), _data.pLevel);

        //
        _slotHP.Init(ECharacterStats.HP, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.HP));
        _slotATK.Init(ECharacterStats.ATK, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.ATK));
        _slotDEF.Init(ECharacterStats.DEF, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.DEF));
        _slotAvoid.Init(ECharacterStats.Avoid, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.Avoid));
        _slotFocus.Init(ECharacterStats.Focus, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.Focus));
        _slotAtkSpd.Init(ECharacterStats.AtkSpd, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.AtkSpd));
        _slotSpeed.Init(ECharacterStats.Speed, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.Speed));
        _slotCriRate.Init(ECharacterStats.Crirate, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.Crirate));
        _slotCriDmg.Init(ECharacterStats.Cridmg, Manager_Character.Instance.GetTotalStat(_data, ECharacterStats.Cridmg));
        
        _slotActive.Init(_data.pTableInfo.activeSkill);
        _slotPassive_0.Init(_data.pTableInfo.passiveSkill_0);
        _slotPassive_1.Init(_data.pTableInfo.passiveSkill_1);

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
    void OnBtnSlot(Com_Characters_Slot slot)
    {
        //
        _data = slot.pData;

        //
        foreach (var item in pSlots)
        {
            item.SetSelected(item == slot);
        }        

        //
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnBtnClose()
    {
        base.OnBtnClose();
    }
}
