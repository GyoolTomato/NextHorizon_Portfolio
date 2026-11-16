using UnityEngine;
using UnityEngine.UI;
using System;

public class Com_CharacterSelect_Slot : Com_Base
{
    //
    [SerializeField] GameObject _frameSelected;
    [SerializeField] Image _portrait;

    //
    _102_Character.Values _tableInfo;
    Action<_102_Character.Values> _onBtn;


    /// <summary>
    /// 
    /// </summary>
    public void Init(_102_Character.Values tableInfo, Action<_102_Character.Values> onBtn)
    {
        //
        base.Init();

        //
        _tableInfo = tableInfo;
        _onBtn = onBtn;

        _portrait.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.Portrait, _tableInfo.model);

        //
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetActiveSelectedFrame(int selectedCharacterKey)
    {
        _frameSelected.SetActive(_tableInfo.key ==  selectedCharacterKey);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        _onBtn?.Invoke(_tableInfo);
    }
}
