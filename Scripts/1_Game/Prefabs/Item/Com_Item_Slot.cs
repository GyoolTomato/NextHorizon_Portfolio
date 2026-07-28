using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Com_Item_Slot : Com_Base
{   
    //
    [SerializeField] Com_Item_Currency_Slot _currencySlot = null;
    [SerializeField] Com_Item_Armor_Slot _armorSlot = null;
    [SerializeField] Com_Item_Weapon_Slot _weaponSlot = null;

    //
    public enum ESlotType
    {
        None,
        Currency,
        Armor,
        Weapon
    }

    //
    public ESlotType pSelectedType { private set; get; } = ESlotType.Currency;

    //
    Action<_101_Items.Values> _onBtnCurrency = null;
    Action<_105_Armors.Values> _onBtnArmor = null;
    Action<_106_Weapons.Values> _onBtnWeapon = null;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="curTbleInfo"></param>
    public void Init(_101_Items.Values curTbleInfo, long value, Action<_101_Items.Values> onBtn = null)
    {
        //
        _currencySlot.gameObject.SetActive(true);
        _armorSlot.gameObject.SetActive(false);
        _weaponSlot.gameObject.SetActive(false);

        //
        pSelectedType = ESlotType.Currency;
        _currencySlot.Init(curTbleInfo, value);
        _onBtnCurrency = onBtn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="armTableInfo"></param>
    public void Init(_105_Armors.Values armTableInfo, Action<_105_Armors.Values> onBtn = null)
    {
        //
        _currencySlot.gameObject.SetActive(false);
        _armorSlot.gameObject.SetActive(true);
        _weaponSlot.gameObject.SetActive(false);

        //
        pSelectedType = ESlotType.Armor;
        _armorSlot.Init(armTableInfo, 0);
        _onBtnArmor = onBtn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wepTableInfo"></param>
    public void Init(_106_Weapons.Values wepTableInfo, Action<_106_Weapons.Values> onBtn = null)
    {
        //
        _currencySlot.gameObject.SetActive(false);
        _armorSlot.gameObject.SetActive(false);
        _weaponSlot.gameObject.SetActive(true);

        //
        pSelectedType = ESlotType.Weapon;
        _weaponSlot.Init(wepTableInfo, 0);
        _onBtnWeapon = onBtn;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Refresh()
    {
        switch (pSelectedType)
        {
            case ESlotType.Currency:
                _currencySlot.Init(_currencySlot.pTableInfo, GlobalData.Instance.pDataPlayerInfo.GetItemCount(_currencySlot.pTableInfo.type));
                break;
            case ESlotType.Armor:
                _armorSlot.Init(_armorSlot.pTableInfo, GlobalData.Instance.pDataPlayerInfo.GetItemCount(_currencySlot.pTableInfo.type));
                break;
            case ESlotType.Weapon:
                _weaponSlot.Init(_weaponSlot.pTableInfo, GlobalData.Instance.pDataPlayerInfo.GetItemCount(_currencySlot.pTableInfo.type));
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        switch (pSelectedType)
        {
            case ESlotType.Currency:
                if (_onBtnCurrency != null)
                {
                    _onBtnCurrency.Invoke(_currencySlot.pTableInfo);
                }
                else
                {
                    var panel = Manager_UI.Instance.ShowPanel(EPanelType.PopUpInfo) as Panel_PopUpInfo;
                    panel.Init(transform.position, string.Empty, string.Empty);
                }
                break;
            case ESlotType.Armor:
                if (_onBtnArmor != null)
                {
                    _onBtnArmor.Invoke(_armorSlot.pTableInfo);
                }
                else
                {
                    var panel = Manager_UI.Instance.ShowPanel(EPanelType.PopUpInfo) as Panel_PopUpInfo;
                    panel.Init(transform.position, string.Empty, string.Empty);
                }
                break;
            case ESlotType.Weapon:
                if (_onBtnWeapon != null)
                {
                    _onBtnWeapon.Invoke(_weaponSlot.pTableInfo);
                }
                else
                {
                    var panel = Manager_UI.Instance.ShowPanel(EPanelType.PopUpInfo) as Panel_PopUpInfo;
                    panel.Init(transform.position, string.Empty, string.Empty);
                }
                break;
        }
    }
}
