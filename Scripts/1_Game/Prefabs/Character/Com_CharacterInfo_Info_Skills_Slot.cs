using System;
using UnityEngine;
using UnityEngine.UI;

public class Com_CharacterInfo_Info_Skills_Slot : Com_Base
{
    //    
    [SerializeField] GameObject _activeSkillFrame;
    [SerializeField] GameObject _passiveSkillFrame;
    [SerializeField] Image _icon = null;
    

    //
    _103_CharacterSkills.Values _tableInfo = null;


    /// <summary>
    /// 
    /// </summary>
    public void Init(int key)
    {
        //
        _tableInfo = _103_CharacterSkills.GetItem(key);

        //
        _activeSkillFrame.SetActive(_tableInfo.type == ESkillType.ActiveSkill);
        _passiveSkillFrame.SetActive(_tableInfo.type == ESkillType.PassiveSkill);
        _icon.sprite = Manager_Resources.Instance.GetIconSprite(_tableInfo.key);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        //
        var header = string.Empty;
        var colorCode = string.Empty;   
        switch (_tableInfo.type)
        {
            case ESkillType.ActiveSkill:
                header = "<color=#9932CC>[액티브 스킬]</color>";
                colorCode = "#9932CC";
                break;
            case ESkillType.PassiveSkill:
                header = "<color=#4169E1>[패시브 스킬]</color>";
                colorCode = "#4169E1";
                break;
        }
        var desc = header + " " + Manager_UI.Instance.GetSkillDesc(_tableInfo);

        var panel = Manager_UI.Instance.ShowPanel(EPanelType.PopUpInfo) as Panel_PopUpInfo;
        panel.Init(transform.position, Manager_UI.Instance.GetTextSkill(_tableInfo.title), desc, colorCode);
    }
}
