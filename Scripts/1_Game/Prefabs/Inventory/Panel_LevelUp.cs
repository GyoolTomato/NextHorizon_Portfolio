using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Panel_LevelUp : Panel_Slots<Com_Item_Slot>, Observer.IObserver<Observer.CharacterLevelUpEvent>, Observer.IObserver<Observer.EquipmentLevelUpEvent>
{
    //
    enum EType
    { 
        None,
        Character,
        Weapon,
        Armor
    }

    //
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] TextMeshProUGUI _tempLevel;
    [SerializeField] TextMeshProUGUI _exp;
    [SerializeField] RectTransform _expGauge;
    [SerializeField] SubPanel_CharacterInfo_LevelUp_Btns _comItemBtns;
    [SerializeField] Com_Button _btnCancel;
    [SerializeField] Com_Button _btnConfirm;

    //
    EType _type = EType.None;
    DataCharacter _character;
    DataWeapon _weapon;
    DataArmor _armor;

    Com_Item_Slot _slotAlready;
    Com_Item_Slot _slotBtn;
    float _pointDownInterval;
    float _autoStartTime;
    long _cardExp;
    _107_CharacterLevel.Values _tableCharacterLevel;
    _111_WeaponLevel.Values _tableWeaponLevel;
    _112_ArmorLevel.Values _tableArmorLevel;

    Action _onLevelUpEvent;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.LevelUp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public void OnEvent(Observer.CharacterLevelUpEvent msg)
    {
        _onLevelUpEvent?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public void OnEvent(Observer.EquipmentLevelUpEvent msg)
    {
        _onLevelUpEvent?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        base.Init();

        //
        _slotAlready = null;
        _slotBtn = null;
        _pointDownInterval = 0.05f;
        _autoStartTime = 0f;

        //
        DeactiveSlots();

        //
        _btnCancel.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(DataCharacter character, Action onLevelUpEvent)
    {
        //
        Init();

        //
        _type = EType.Character;
        _character = character;      
        _onLevelUpEvent = onLevelUpEvent;

        //
        _tableCharacterLevel = Manager_Table.Instance.GetCharacterLevelInfo(_character.pLevel);
        if (_tableCharacterLevel == null)
        {
            Debug.LogError($"Level Table Info is null for level {_character.pLevel}");
            return;
        }

        //
        _comItemBtns.Init(new EItemType[] { EItemType.ExpCard }, OnPointerDown, OnPointerPressing_Btn, OnPointerUp_Btn);

        //
        SetExpValue();

        //
        Observer.ObserverTracker<Observer.CharacterLevelUpEvent>.Instance.Subscribe(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="onLevelUpEvent"></param>
    public void Init(DataWeapon data, Action onLevelUpEvent)
    {
        //
        Init();

        //
        _type = EType.Weapon;
        _weapon = data;
        _onLevelUpEvent = onLevelUpEvent;

        //
        _tableWeaponLevel = Manager_Table.Instance.GetWeaponLevelInfo(_weapon.pLevel);
        if (_tableWeaponLevel == null)
        {
            Debug.LogError($"Level Table Info is null for level {_weapon.pLevel}");
            return;
        }

        //
        _comItemBtns.Init(new EItemType[] { EItemType.ReinforcementCard }, OnPointerDown, OnPointerPressing_Btn, OnPointerUp_Btn);

        //
        SetExpValue();

        //
        Observer.ObserverTracker<Observer.EquipmentLevelUpEvent>.Instance.Subscribe(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="onLevelUpEvent"></param>
    public void Init(DataArmor data, Action onLevelUpEvent)
    {
        //
        Init();

        //
        _type = EType.Armor;
        _armor = data;
        _onLevelUpEvent = onLevelUpEvent;

        //
        _tableArmorLevel = Manager_Table.Instance.GetArmorLevelInfo(_armor.pLevel);
        if (_tableArmorLevel == null)
        {
            Debug.LogError($"Level Table Info is null for level {_armor.pLevel}");
            return;
        }

        //
        _comItemBtns.Init(new EItemType[] { EItemType.ReinforcementCard }, OnPointerDown, OnPointerPressing_Btn, OnPointerUp_Btn);

        //
        SetExpValue();
        //
        Observer.ObserverTracker<Observer.EquipmentLevelUpEvent>.Instance.Subscribe(this);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Hide()
    {
        //
        Observer.ObserverTracker<Observer.CharacterLevelUpEvent>.Instance.Unsubscribe(this);
        Observer.ObserverTracker<Observer.EquipmentLevelUpEvent>.Instance.Unsubscribe(this);

        //
        base.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Com_Item_Slot GetSlot(EItemType itemType)
    {
        //
        if (_slotAlready == null || _slotAlready.pData.pTableInfo.type != itemType)
        {
            foreach (var item in pSlots)
            {
                if (item.pData.pTableInfo.type == itemType)
                {
                    _slotAlready = item;
                    break;
                }
            }
        }

        //
        return _slotAlready;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    void SetAlreadyItem(EItemType type, bool isAdd)
    {
        //
        var data = GameData.Instance.pDataInventory.GetDataItem(type);
        _slotAlready = GetSlot(type);
        _slotBtn = _comItemBtns.GetSlot(type, _slotBtn);

        //
        if (isAdd)
        {
            //
            if (_slotBtn.pData.pCount <= 0)
            {
                return;
            }

            //
            if (_slotAlready == null)
            {
                var slot = ActivateSlot();
                slot.Init(new DataItem(0, _slotBtn.pData.pTableInfo), EItemValueType.Name);
                slot.SetPointerControl(OnPointerDown, OnPointerPressing_Already, OnPointerUp_Already);
                _slotAlready = slot;
            }

            //
            if (_slotAlready.pData.pCount >= data.pCount || _slotBtn.pData.pCount <= 0)
                return;

            //
            _slotAlready.pData.AddItemCount(1);
            _slotBtn.pData.SubtractItemCount(1);

            _slotAlready.Refresh();
            _slotBtn.Refresh();
        }
        else
        {
            //
            if (_slotAlready == null)
                return;

            //
            if (_slotAlready.pData.pCount <= 0 || _slotBtn.pData.pCount > data.pCount)
                return;

            //
            _slotAlready.pData.SubtractItemCount(1);
            _slotBtn.pData.AddItemCount(1);

            _slotAlready.Refresh();
            _slotBtn.Refresh();

            //
            if (_slotAlready.pData.pCount == 0)
            {                
                DeactiveSlot(_slotAlready);
                _slotAlready = null;
            }
        }

        SetExpValue();
    }

    /// <summary>
    /// 
    /// </summary>
    void SetExpValue()
    {
        //
        var level = 0;
        var tempLevel = 0;
        var remainExp = 0L;
        var expToNextLevel = 0;
        var gaugeScale = 0d;
        var isResultMaxLevel = false;

        //
        var expTypes = new List<EItemType>();
        var expCount = new List<long>();

        foreach (var item in pSlots)
        {
            expTypes.Add(item.pData.pTableInfo.type);
            expCount.Add(item.pData.pCount);
        }

        //
        switch (_type)
        {
            case EType.Character:
                Manager_Character.Instance.GetExpectLevel(_character.pLevel, _character.pExp, expTypes.ToArray(), expCount.ToArray(), out var resultTableInfo_character, out _cardExp, out remainExp);

                level = _character.pLevel;
                isResultMaxLevel = Manager_Character.Instance.IsMaxLevel(resultTableInfo_character.level);
                tempLevel = resultTableInfo_character.level;
                expToNextLevel = resultTableInfo_character.expToNextLevel;
                break;
            case EType.Weapon:
                Manager_Inventory.Instance.GetExpectLevel_Weapon(_weapon.pLevel, _weapon.pExp, expTypes.ToArray(), expCount.ToArray(), out var resultTableInfo_weapon, out _cardExp, out remainExp);

                level = _weapon.pLevel;
                isResultMaxLevel = Manager_Character.Instance.IsMaxLevel(resultTableInfo_weapon.level);
                tempLevel = resultTableInfo_weapon.level;
                expToNextLevel = resultTableInfo_weapon.expToNextLevel;
                break;
            case EType.Armor:
                Manager_Inventory.Instance.GetExpectLevel_Armor(_armor.pLevel, _armor.pExp, expTypes.ToArray(), expCount.ToArray(), out var resultTableInfo_armor, out _cardExp, out remainExp);

                level = _armor.pLevel;
                isResultMaxLevel = Manager_Character.Instance.IsMaxLevel(resultTableInfo_armor.level);
                tempLevel = resultTableInfo_armor.level;
                expToNextLevel = resultTableInfo_armor.expToNextLevel;
                break;
        }

        //
        gaugeScale = isResultMaxLevel ? 1 : Convert.ToDouble(remainExp) / Convert.ToDouble(expToNextLevel);
        if (gaugeScale > 1)
            gaugeScale = 1;

        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), level);
        _tempLevel.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), tempLevel);
        _exp.text = isResultMaxLevel ? "MAX" : string.Format("{0} / {1}", remainExp, expToNextLevel);
        _expGauge.localScale = new Vector3(Convert.ToSingle(gaugeScale), 1, 1);

        _btnConfirm.SetActive(pSlots.Count > 0);
    }   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerDown(Com_Item_Slot slot)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerPressing(Com_Item_Slot slot, bool isAdd)
    {
        if (slot.pPressingTime > 0.3f)
        {
            //
            if (_autoStartTime <= slot.pPressingTime)
            {
                _autoStartTime = slot.pPressingTime + _pointDownInterval;

                SetAlreadyItem(slot.pData.pTableInfo.type, isAdd);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerUp(Com_Item_Slot slot, bool isAdd)
    {
        //
        _autoStartTime = 0f;

        //
        SetAlreadyItem(slot.pData.pTableInfo.type, isAdd);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerPressing_Already(Com_Item_Slot slot)
    {
        OnPointerPressing(slot, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerUp_Already(Com_Item_Slot slot)
    {
        OnPointerUp(slot, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerPressing_Btn(Com_Item_Slot slot)
    {
        OnPointerPressing(slot, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    void OnPointerUp_Btn(Com_Item_Slot slot)
    {
        OnPointerUp(slot, true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLevelUp()
    {
        //
        var eItemTypes = new List<EItemType>();
        var counts = new List<long>();

        foreach (var item in pSlots)
        {
            if (item.pData.pCount <= 0)
                continue;

            eItemTypes.Add(item.pData.pTableInfo.type);
            counts.Add(item.pData.pCount);
        }

        //
        if (eItemTypes.Count == 0)
            return;

        //
        switch (_type)
        {
            case EType.Character:
                {
                    ServerAPI.Instance.Send_CharacterLevelUp(_character.pTableInfo.key, eItemTypes.ToArray(), counts.ToArray(), (isSuccess) =>
                    {
                        if (isSuccess)
                        {
                            Manager_UI.Instance.ShowFlash(() =>
                            {
                                Init(_character, _onLevelUpEvent);
                                Manager_UI.Instance.GetPanel(EPanelType.CharacterInfo).Refresh();
                                Manager_UI.Instance.GetPanel(EPanelType.Characters).Refresh();
                            });
                        }
                    }, (error) => { });
                }
                break;
            case EType.Weapon:
                {
                    ServerAPI.Instance.Send_EquipmentLevelUp(_weapon.pId, false, eItemTypes.ToArray(), counts.ToArray(), (isSuccess) =>
                    {
                        Manager_UI.Instance.ShowFlash(() =>
                        {
                            Init(_weapon, _onLevelUpEvent);
                            Manager_UI.Instance.GetPanel(EPanelType.CharacterInfo).Refresh();
                            Manager_UI.Instance.GetPanel(EPanelType.Characters).Refresh();
                        });
                    }, (error) => { });
                }
                break;
            case EType.Armor:
                {
                    ServerAPI.Instance.Send_EquipmentLevelUp(_armor.pId, true, eItemTypes.ToArray(), counts.ToArray(), (isSuccess) =>
                    {
                        Manager_UI.Instance.ShowFlash(() =>
                        {
                            Init(_armor, _onLevelUpEvent);
                            Manager_UI.Instance.GetPanel(EPanelType.CharacterInfo).Refresh();
                            Manager_UI.Instance.GetPanel(EPanelType.Characters).Refresh();
                        });
                    }, (error) => { });
                }
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Tick()
    {
        //
        if (_slotAlready != null)
            _slotAlready.Tick();

        if (_slotBtn != null)
            _slotBtn.Tick();
    }
}
