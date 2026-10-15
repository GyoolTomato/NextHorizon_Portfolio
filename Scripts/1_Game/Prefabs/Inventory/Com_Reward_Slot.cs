using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Com_Reward_Slot : Com_Base
{
    //
    enum EType
    {
        None,
        Item,
        Armor,
        Weapon,
    }

    //
    [SerializeField] Com_Item_Slot _comItem;
    [SerializeField] Com_Armor_Slot _comArmor;
    [SerializeField] Com_Weapon_Slot _comWeapon;

    //
    EType _nowType = EType.None;
    Action<DataItem> _onBtn_Item;
    Action<DataArmor> _onBtn_Armor;
    Action<DataWeapon> _onBtn_Weapon;

    //
    public Com_Item_Slot pComItem => _comItem;
    public Com_Armor_Slot pComArmor => _comArmor;
    public Com_Weapon_Slot pComWeapon => _comWeapon;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataItem data, EItemValueType type, Action<Com_Item_Slot> onPointerUp = null)
    {
        _nowType = EType.Item;

        _comItem.gameObject.SetActive(true);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(data, type);
        _comItem.SetPointerControl(null, null, onPointerUp);
        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init((DataWeapon)null, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    /// <param name="quantity"></param>
    public void Init(_101_Items.Values tableInfo, EItemValueType type, long quantity, Action<Com_Item_Slot> onPointerUp = null)
    {
        _nowType = EType.Item;

        _comItem.gameObject.SetActive(true);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(new DataItem(quantity, tableInfo), type);
        _comItem.SetPointerControl(null, null, onPointerUp);
        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init((DataWeapon)null, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataArmor data, EItemValueType type, Action<Com_Armor_Slot> onPointerUp = null)
    {
        _nowType = EType.Armor;

        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(null, type);
        _comArmor.Init(data, type);
        _comArmor.SetPointerControl(null, null, onPointerUp);
        _comWeapon.Init((DataWeapon)null, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_105_Armors.Values tableInfo, EItemValueType type, Action<Com_Armor_Slot> onPointerUp = null)
    {
        _nowType = EType.Armor;

        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(null, type);
        _comArmor.Init(tableInfo, type);
        _comArmor.SetPointerControl(null, null, onPointerUp);
        _comWeapon.Init((DataWeapon)null, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataWeapon data, EItemValueType type, Action<Com_Weapon_Slot> onPointerUp = null)
    {
        _nowType = EType.Weapon;

        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comItem.Init(null, type);
        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init(data, type);
        _comWeapon.SetPointerControl(null, null, onPointerUp);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_106_Weapons.Values tableInfo, EItemValueType type, Action<Com_Weapon_Slot> onPointerUp = null)
    {
        _nowType = EType.Weapon;

        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comItem.Init(null, type);
        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init(tableInfo, type);
        _comWeapon.SetPointerControl(null, null, onPointerUp);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        //
        switch (_nowType)
        {
            case EType.Item  : _onBtn_Item  ?.Invoke(_comItem.pData  ); break;
            case EType.Armor : _onBtn_Armor ?.Invoke(_comArmor.pData ); break;
            case EType.Weapon: _onBtn_Weapon?.Invoke(_comWeapon.pData); break;
        }
    }
}
