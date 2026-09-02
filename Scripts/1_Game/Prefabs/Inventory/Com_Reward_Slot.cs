using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Com_Reward_Slot : Com_Base
{
    //
    [SerializeField] Com_Item_Slot _comItem;
    [SerializeField] Com_Armor_Slot _comArmor;
    [SerializeField] Com_Weapon_Slot _comWeapon;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataItem data, EItemValueType type)
    {
        _comItem.gameObject.SetActive(true);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(data, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    /// <param name="quantity"></param>
    public void Init(_101_Items.Values tableInfo, EItemValueType type, long quantity)
    {
        _comItem.gameObject.SetActive(true);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(false);

        _comItem.Init(new DataItem(quantity, tableInfo), type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataArmor data, EItemValueType type)
    {
        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comArmor.Init(data, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_105_Armors.Values tableInfo, EItemValueType type)
    {
        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comArmor.Init(tableInfo, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataWeapon data, EItemValueType type)
    {
        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comWeapon.Init(data, type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_106_Weapons.Values tableInfo, EItemValueType type)
    {
        _comItem.gameObject.SetActive(false);
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comWeapon.Init(tableInfo, type);
    }
}
