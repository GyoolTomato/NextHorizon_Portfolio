using Observer;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WearEquipment : Panel_Slots<Com_Equipment_Slot>, 
    Observer.IObserver<Observer.ArmorEquippedEvent>, 
    Observer.IObserver<Observer.WeaponEquippedEvent>
{
    //
    [SerializeField] Com_ContentsTitle _comTitle;
    [SerializeField] Com_WearEquipment_Equip _comEquipNow;
    [SerializeField] Com_WearEquipment_Equip _comEquipSelected;
    [SerializeField] Com_Button _comBtnUnequip;
    [SerializeField] Com_Button _comBtnConfirm;

    //
    Com_Equipment_Slot _equipedSlot;
    Com_Equipment_Slot _selectedSlot;

    int _characterKey;
    EEquipmentType _lockType;
    Action _onHide;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.WearEquipment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public void OnEvent(Observer.ArmorEquippedEvent msg)
    {
        Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public void OnEvent(Observer.WeaponEquippedEvent msg)
    {
        Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <summary>
    /// 
    /// </summary>
    public void Init(int characterKey, EEquipmentType type, Action onHide)
    {
        //
        base.Init();

        //
        _characterKey = characterKey;
        _lockType = type;
        _onHide = onHide;

        //
        _equipedSlot = null;
        _selectedSlot = null;

        //
        _comTitle.Init(OnBtnClose);        
        _comBtnUnequip.SetBtn(OnBtnRelease);
        _comBtnConfirm.SetActive(false);
        _comBtnConfirm.SetBtn(OnBtnConfirm);        

        //
        switch (_lockType)
        {
            case EEquipmentType.Weapon:
                //
                var dataWeapon = GameData.Instance.pDataInventory.GetCharacterWeapon(characterKey);
                _comEquipNow.Init(dataWeapon);
                _comEquipSelected.Init((DataWeapon)null);

                //
                DeactiveSlots();

                foreach (var item in GameData.Instance.pDataInventory.pDicWeapons)
                {
                    //
                    var slot = ActivateSlot();
                    slot.Init(item.Value, EItemValueType.Name, OnClick);

                    //
                    if (dataWeapon != null && dataWeapon.pId == item.Value.pId)
                    {
                        _equipedSlot = slot;
                        _equipedSlot.SetEquiped(true);
                    }
                }
                break;
            case EEquipmentType.Hat:
            case EEquipmentType.Clothes:
            case EEquipmentType.Gloves:
            case EEquipmentType.Shoes:
                //
                var dataArmor = GameData.Instance.pDataInventory.GetCharacterArmor(characterKey, _lockType);
                _comEquipNow.Init(dataArmor);
                _comEquipSelected.Init((DataArmor)null);

                //
                DeactiveSlots();

                foreach (var item in GameData.Instance.pDataInventory.pDicArmors)
                {
                    //
                    if (_lockType != item.Value.pTableInfo.type)
                    {
                        continue;
                    }

                    //
                    var slot = ActivateSlot();
                    slot.Init(item.Value, EItemValueType.Name, OnClick);

                    //
                    if (dataArmor != null && dataArmor.pId == item.Value.pId)
                    {
                        _equipedSlot = slot;
                        _equipedSlot.SetEquiped(true);
                    }
                }
                break;
        }

        //
        _comBtnUnequip.SetActive(_equipedSlot != null);

        //
        ObserverTracker<ArmorEquippedEvent >.Instance.Subscribe(this);
        ObserverTracker<WeaponEquippedEvent>.Instance.Subscribe(this);
    }

    public override void Hide()
    {
        //
        _onHide?.Invoke();

        //
        ObserverTracker<ArmorEquippedEvent >.Instance.Unsubscribe(this);
        ObserverTracker<WeaponEquippedEvent>.Instance.Unsubscribe(this);

        //
        base.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    void OnClick(Com_Equipment_Slot slot)
    {
        //
        if (_equipedSlot == slot)
        {
            return;
        }

        //
        if (_selectedSlot != null)
            _selectedSlot.SetSelected(false);

        //
        if (_selectedSlot == slot)
        {
            _selectedSlot = null;
        }
        else
        {
            _selectedSlot = slot;
            _selectedSlot.SetSelected(true);
        }

        //
        switch (_lockType)
        {
            case EEquipmentType.Weapon:
                _comEquipSelected.Init(_selectedSlot != null ? _selectedSlot.pComWeapon.pData : null);
                break;
            case EEquipmentType.Hat:
            case EEquipmentType.Clothes:
            case EEquipmentType.Gloves:
            case EEquipmentType.Shoes:
                _comEquipSelected.Init(_selectedSlot != null ? _selectedSlot.pComArmor.pData : null);
                break;
        }

        //
        _comBtnConfirm.SetActive(_selectedSlot != null);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnRelease()
    {
        switch (_lockType)
        {
            case EEquipmentType.Weapon:
                ServerAPI.Instance.Send_WeaponEquip(_characterKey, -1, (isSuccess) =>
                {

                }, (error) =>
                {

                });
                break;
            case EEquipmentType.Hat:
            case EEquipmentType.Clothes:
            case EEquipmentType.Gloves:
            case EEquipmentType.Shoes:
                ServerAPI.Instance.Send_ArmorEquip(_characterKey, _lockType, -1, (isSuccess) =>
                {

                }, (error) =>
                {

                });
                break;
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnConfirm()
    {
        //
        if (_equipedSlot == null && _selectedSlot == null)
        {
            Hide();
            return;
        }

        //
        switch (_lockType)
        {
            case EEquipmentType.Weapon:
                ServerAPI.Instance.Send_WeaponEquip(_characterKey, _selectedSlot.pComWeapon.pData.pId, (isSuccess) =>
                {

                }, (error) =>
                {

                });
                break;
            case EEquipmentType.Hat:
            case EEquipmentType.Clothes:
            case EEquipmentType.Gloves:
            case EEquipmentType.Shoes:
                ServerAPI.Instance.Send_ArmorEquip(_characterKey, _lockType, _selectedSlot.pComArmor.pData.pId, (isSuccess) =>
                {

                }, (error) =>
                {

                });
                break;
            default:
                break;
        }        
    }
}
