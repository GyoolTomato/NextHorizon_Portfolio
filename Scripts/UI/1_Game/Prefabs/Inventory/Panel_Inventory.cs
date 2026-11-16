using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Inventory : Panel_Base
{
    //
    public enum EInventoryType
    {
        None,
        Items,
        Armors,
        Weapons,
    }

    //
    [SerializeField] Com_ContentsTitle _title;
    [SerializeField] Com_Inventory_Btn _btnItems;
    [SerializeField] Com_Inventory_Btn _btnArmors;
    [SerializeField] Com_Inventory_Btn _btnWeapons;

    [SerializeField] Com_Inventory_Items   _comItems;
    [SerializeField] Com_Inventory_Armors  _comArmors;
    [SerializeField] Com_Inventory_Weapons _comWeapons;

    [SerializeField] GameObject _textEmpty;

    //
    EInventoryType _selectedType = EInventoryType.Items;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.Inventory;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        _title     .Init(OnBtnClose);
        _btnItems  .Init(EInventoryType.Items  , OnBtnTab);
        _btnArmors .Init(EInventoryType.Armors , OnBtnTab);
        _btnWeapons.Init(EInventoryType.Weapons, OnBtnTab);

        //
        OnBtnTab(_selectedType);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnTab(EInventoryType inventoryType)
    {
        //
        _selectedType = inventoryType;

        //
        _btnItems  .SetSelected(_selectedType == EInventoryType.Items  );
        _btnArmors .SetSelected(_selectedType == EInventoryType.Armors );
        _btnWeapons.SetSelected(_selectedType == EInventoryType.Weapons);

        _comItems  .gameObject.SetActive(_selectedType == EInventoryType.Items  );
        _comArmors .gameObject.SetActive(_selectedType == EInventoryType.Armors );
        _comWeapons.gameObject.SetActive(_selectedType == EInventoryType.Weapons);

        //
        switch (_selectedType)
        {
            case EInventoryType.Items  : _comItems  .Init(_textEmpty); break;
            case EInventoryType.Armors : _comArmors .Init(_textEmpty); break;
            case EInventoryType.Weapons: _comWeapons.Init(_textEmpty); break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnBtnClose()
    {
        base.OnBtnClose();
    }
}
