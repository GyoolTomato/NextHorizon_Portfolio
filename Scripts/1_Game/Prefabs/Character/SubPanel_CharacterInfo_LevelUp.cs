using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System;

public class SubPanel_CharacterInfo_LevelUp : Com_Slots<Com_Item_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] TextMeshProUGUI _tempLevel;
    [SerializeField] TextMeshProUGUI _exp;
    [SerializeField] RectTransform _expGauge;
    [SerializeField] SubPanel_CharacterInfo_LevelUp_Btns _comItemBtns;
    [SerializeField] Com_Button _btnCancel;
    [SerializeField] Com_Button _btnConfirm;

    //
    Character _character;
    Com_Item_Slot _slotAlready;
    Com_Item_Slot _slotBtn;
    float _pointDownInterval;
    float _autoStartTime;
    long _cardExp;
    _107_CharacterLevel.Values _tableCharacterLevel;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(Character character)
    {
        //
        _character = character;
        _slotAlready = null;
        _slotBtn = null;
        _pointDownInterval = 0.05f;
        _autoStartTime = 0f;

        //
        _tableCharacterLevel = Manager_Table.Instance.GetCharacterLevelInfo(_character.pLevel);
        if (_tableCharacterLevel == null)
        {
            Debug.LogError($"Level Table Info is null for level {_character.pLevel}");
            return;
        }

        //
        DeactiveSlots();
        _comItemBtns.Init(new EItemType[] { EItemType.ExpCard }, OnPointerDown, OnPointerPressing_Btn, OnPointerUp_Btn);

        //
        _tableCharacterLevel = Manager_Table.Instance.GetCharacterLevelInfo(character.pLevel);

        //
        SetExpValue();

        //
        _btnCancel.SetActive(true);
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
        var expTypes = new List<EItemType>();
        var expCount = new List<long>();

        foreach (var item in pSlots)
        {
            expTypes.Add(item.pData.pTableInfo.type);
            expCount.Add(item.pData.pCount);
        }

        Manager_Character.Instance.GetExpectLevel(_character.pLevel, _character.pExp, expTypes.ToArray(), expCount.ToArray(), out var resultTableInfo, out _cardExp, out var remainExp);

        
        var isResultMaxLevel = Manager_Character.Instance.IsMaxLevel(resultTableInfo.level);
        var gaugeScale = isResultMaxLevel ? 1 : Convert.ToDouble(remainExp) / Convert.ToDouble(resultTableInfo.expToNextLevel);
        if (gaugeScale > 1)
            gaugeScale = 1;

        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), _character.pLevel);
        _tempLevel.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), resultTableInfo.level);
        _exp.text = isResultMaxLevel ? "MAX" : string.Format("{0} / {1}", remainExp, resultTableInfo.expToNextLevel);
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
    void OnBtnLevelUp()
    {
        var eItemTypes = new List<EItemType>();
        var counts = new List<long>();

        foreach (var item in pSlots)
        {
            if (item.pData.pCount <= 0)
                continue;

            eItemTypes.Add(item.pData.pTableInfo.type);
            counts.Add(item.pData.pCount);
        }

        if (eItemTypes.Count == 0)
            return;

        ServerAPI.Instance.Send_CharacterLevelUp(GameData.Instance.pPlayerInfo.pUserId, _character.pTableInfo.key, eItemTypes.ToArray(), counts.ToArray(), (isSuccess)=>
        {
            if (isSuccess)
            {
                Manager_UI.Instance.ShowFlash(() =>
                {
                    Init(_character);
                    Manager_UI.Instance.GetPanel(EPanelType.CharacterInfo).Refresh();
                    Manager_UI.Instance.GetPanel(EPanelType.Characters).Refresh();
                });
            }
        },
        (error)=>
        {

        });
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
