using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterInfo : Panel_Base
{
    //
    [SerializeField] TextMeshProUGUI                _name;
    [SerializeField] Image                          _characterImage;
    [SerializeField] TextMeshProUGUI                _level;    
    [SerializeField] Com_ContentsTitle              _comTitle;
    [SerializeField] Com_CharacterInfo_Info_Stats   _comStats;
    [SerializeField] Com_CharacterInfo_Info_Skills  _comSkills;

    [SerializeField] SubPanel_CharacterInfo_LevelUp _subLevelUp;

    //
    Character _character = null;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.CharacterInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(Character character)
    {
        //
        _comTitle.Init(OnBtnClose);
        _subLevelUp.gameObject.SetActive(false);

        //
        _character = character;

        //
        _name.text = Manager_UI.Instance.GetTextCharacter(_character.pTableInfo.name);
        _characterImage.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.FullBody, _character.pTableInfo.model);
        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), _character.pLevel);
        _comStats.Init(_character);
        _comSkills.Init(_character);        
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnBtnClose()
    {
        base.OnBtnClose();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLevelUp()
    {
        _subLevelUp.Init(_character);
        _subLevelUp.gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnCloseLevelUp()
    {
        _subLevelUp.gameObject.SetActive(false);
    }
}