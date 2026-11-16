using System;
using UnityEngine;

public class Panel_CharacterSelect : Panel_Slots<Com_CharacterSelect_Slot>
{
    //
    [SerializeField] Com_Button _comBtn;

    //
    Action<_102_Character.Values> _onBtn;

    _102_Character.Values _selectedCharacter;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.CharacterSelect;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(_102_Character.Values defaultTableInfo, Action<_102_Character.Values> onBtn)
    {
        //
        base.Init();

        //
        _selectedCharacter = defaultTableInfo;
        _onBtn = onBtn;

        //
        _comBtn.SetActive(true);
        _comBtn.SetBtn(OnBtn);

        //
        DeactiveSlots();

        foreach (var item in GD.pDataCharacter.pCharacters)
        {
            if (item.pIsActive)
            {
                var slot = ActivateSlot();
                slot.Init(item.pTableInfo, OnBtnSlot);
                slot.SetActiveSelectedFrame(_selectedCharacter != null ? _selectedCharacter.key : -1);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        foreach (var item in pSlots)
        {
            item.SetActiveSelectedFrame(_selectedCharacter.key);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    void OnBtnSlot(_102_Character.Values tableInfo)
    {
        //
        _selectedCharacter = tableInfo;

        //
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtn()
    {
        _onBtn?.Invoke(_selectedCharacter);
        Hide();
    }
}
