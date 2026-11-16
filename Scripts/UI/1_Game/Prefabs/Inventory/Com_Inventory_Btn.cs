using UnityEngine;
using UnityEngine.UI;
using System;

public class Com_Inventory_Btn : Com_Base
{
    //
    [SerializeField] GameObject _bgSelected = null;

    //   
    Action<Panel_Inventory.EInventoryType> _onClick = null;

    //
    public Panel_Inventory.EInventoryType pInventoryType { private set; get; } = Panel_Inventory.EInventoryType.None;


    /// <summary>
    /// 
    /// </summary>
    public void Init(Panel_Inventory.EInventoryType inventoryType, Action<Panel_Inventory.EInventoryType> onClick)
    {
        //        
        _onClick = onClick;

        //
        pInventoryType = inventoryType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelected(bool isSelected)
    {
        _bgSelected.SetActive(isSelected);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClick()
    {
        _onClick?.Invoke(pInventoryType);
    }
}
