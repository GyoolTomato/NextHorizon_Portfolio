using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Com_Equipment_Slot : Com_Base
{
    //
    [SerializeField] Com_Armor_Slot _comArmor;
    [SerializeField] Com_Weapon_Slot _comWeapon;
    [SerializeField] GameObject _isSelected;
    [SerializeField] GameObject _isEquiped;

    //
    Action<Com_Equipment_Slot> _onBtn;

    //
    public Com_Armor_Slot pComArmor => _comArmor;
    public Com_Weapon_Slot pComWeapon => _comWeapon;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataArmor data, EItemValueType type, Action<Com_Equipment_Slot> onBtn = null)
    {
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comArmor.Init(data, type);
        _comArmor.SetPointerControl(null, null, null);
        _comWeapon.Init((DataWeapon)null, type);

        _onBtn = onBtn;

        SetSelected(false);
        SetEquiped(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_105_Armors.Values tableInfo, EItemValueType type, Action<Com_Equipment_Slot> onBtn = null)
    {
        _comArmor.gameObject.SetActive(true);
        _comWeapon.gameObject.SetActive(false);

        _comArmor.Init(tableInfo, type);
        _comArmor.SetPointerControl(null, null, null);
        _comWeapon.Init((DataWeapon)null, type);

        _onBtn = onBtn;

        SetSelected(false);
        SetEquiped(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataWeapon data, EItemValueType type, Action<Com_Equipment_Slot> onBtn = null)
    {
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init(data, type);
        _comWeapon.SetPointerControl(null, null, null);

        _onBtn = onBtn;

        SetSelected(false);
        SetEquiped(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="type"></param>
    public void Init(_106_Weapons.Values tableInfo, EItemValueType type, Action<Com_Equipment_Slot> onBtn = null)
    {
        _comArmor.gameObject.SetActive(false);
        _comWeapon.gameObject.SetActive(true);

        _comArmor.Init((DataArmor)null, type);
        _comWeapon.Init(tableInfo, type);
        _comArmor.SetPointerControl(null, null, null);

        _onBtn = onBtn;

        SetSelected(false);
        SetEquiped(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        _isSelected.SetActive(isSelected);

        if (isSelected)
        {
            SetEquiped(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetEquiped(bool isEquiped)
    {
        _isEquiped.SetActive(isEquiped);

        if (isEquiped)
        {
            SetSelected(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        //
        _onBtn?.Invoke(this);
    }
}
