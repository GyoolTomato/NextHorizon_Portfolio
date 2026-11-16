using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Com_EquipmentSelct_Slot : Com_Base
{
    //
    [SerializeField] GameObject _emptyText;
    [SerializeField] TextMeshProUGUI _typeText;
    [SerializeField] Com_Equipment_Slot _slot;

    //
    int _characterKey = -1;
    EEquipmentType _lockType = EEquipmentType.None;
    Action _onHide;


    /// <summary>
    /// 
    /// </summary>
    public void Init(int characterKey, EEquipmentType lockType, Action onHide)
    {
        _characterKey = characterKey;
        _lockType = lockType;
        _onHide = onHide;
        _typeText.text = Manager_UI.Instance.GetEquipmentTypeText(lockType);

        switch (_lockType)
        {
            case EEquipmentType.Weapon:
                var dataWeapon = GameData.Instance.pDataInventory.GetCharacterWeapon(characterKey);
                _emptyText.SetActive(dataWeapon == null);
                _typeText.gameObject.SetActive(dataWeapon == null);
                _slot.gameObject.SetActive(dataWeapon != null);                
                _slot.Init(dataWeapon, EItemValueType.Name);
                break;
            case EEquipmentType.Hat:
            case EEquipmentType.Clothes:
            case EEquipmentType.Gloves:
            case EEquipmentType.Shoes:
                var dataArmor = GameData.Instance.pDataInventory.GetCharacterArmor(characterKey, _lockType);
                _emptyText.SetActive(dataArmor == null);
                _typeText.gameObject.SetActive(dataArmor == null);
                _slot.gameObject.SetActive(dataArmor != null);
                _slot.Init(dataArmor, EItemValueType.Name);
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.WearEquipment) as Panel_WearEquipment;
        panel.Init(_characterKey, _lockType, _onHide);
    }
}
