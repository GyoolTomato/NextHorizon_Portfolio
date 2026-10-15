using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_Characters_Slot : Com_Base
{
    //
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _value;
    [SerializeField] GameObject _selectedFrame;

    //    
    Action<Com_Characters_Slot> _onBtnSlot = null;

    //
    public DataCharacter pData = null;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <param name="viewMode"></param>
    /// <param name="onBtnSlot"></param>
    public void Init(DataCharacter character, Panel_Characters.EViewMode viewMode, Action<Com_Characters_Slot> onBtnSlot)
    {
        //
        pData = character;
        if (pData == null)
        {
            return;
        }

        //
        _onBtnSlot = onBtnSlot;
        _image.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.UpperBody, pData.pTableInfo.model);
        //_image.SetNativeSize();
        _name.text = Manager_UI.Instance.GetTextCharacter(pData.pTableInfo.name);

        SetValue(viewMode);
        SetSelected(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        _selectedFrame.SetActive(isSelected);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetValue(Panel_Characters.EViewMode viewMode)
    {
        //
        switch (viewMode)
        {
            case Panel_Characters.EViewMode.Level:
                _value.text = string.Format("Lv. {0}", pData.pLevel);
                break;
            case Panel_Characters.EViewMode.Grade:
                _value.text = string.Format("¡Ú {0}", pData.pGrade);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClick()
    {
        _onBtnSlot?.Invoke(this);
    }
}
