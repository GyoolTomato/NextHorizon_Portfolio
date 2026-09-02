using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Manager_UI : Singleton<Manager_UI>
{
    //
    ELanguage _language;

    //
    Dictionary<EPanelType, Panel_Base> _dicPanels = new Dictionary<EPanelType, Panel_Base>();
    List<Com_Base> _coms = new List<Com_Base>();

    Transform _board_0;
    Transform _board_1;
    Transform _board_2;

    //
    GameObject _panelTitleObj;
    GameObject _panelMessageBoxObj;

    //
    AssetReference SpawnablePrefab;


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        _language = ELanguage.Korean;

        _board_0 = GameObject.Find("Board_0").transform;
        _board_1 = GameObject.Find("Board_1").transform;
        _board_2 = GameObject.Find("Board_2").transform;

        Clear();

        _panelTitleObj = Resources.Load<GameObject>("Prefabs/Panel_Title");
        _panelMessageBoxObj = Resources.Load<GameObject>("Prefabs/Panel_MessageBox");
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        _dicPanels.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CreatePanel(EPanelType panelType)
    {
        GameObject loadObject = null;

        if (panelType == EPanelType.Title)
        {
            loadObject = _panelTitleObj;
        }
        else if (panelType == EPanelType.MessageBox)
        {
            loadObject = _panelMessageBoxObj;
        }
        else
        {
            loadObject = Manager_Addressable.Instance.GetPanel(panelType);
        }

        var createObject = GameObject.Instantiate(loadObject, _board_0);
        if (createObject == null)
        {
            Debug.LogError("CreatePanel Create Fail! Panel Type : " + panelType);
            return false;
        }
        createObject.transform.localPosition = Vector3.zero;

        var panelBase = createObject.GetComponent<Panel_Base>();
        if (panelBase == null)
        {
            Debug.LogError("CreatePanel GetComponent Fail! Panel Type : " + panelType);
            return false;
        }

        _dicPanels.Add(panelType, panelBase);

        return true;
    }

    ///<summary>
    ///
    ///</summary>
    public Panel_Base GetPanel(EPanelType panelType)
    {        
        //
        if (_dicPanels.ContainsKey(panelType) == false)
        {
            CreatePanel(panelType);
        }

        //
        return _dicPanels[panelType];
    }

    ///<summary>
    ///
    ///</summary>
    public Panel_Base ShowPanel(EPanelType panelType)
    {
        //
        var panel = GetPanel(panelType);
        if (panel != null)
        {            
            panel.pIsShow = true;
            panel.OnShowPanel();
        }
        else
        {
            Debug.LogError("ShowPanel GetPanel Fail! Panel Type : " + panelType);
        }

        //
        return panel;
    }

    /// <summary>
    /// 
    /// </summary>
    public void HidePanel(EPanelType panelType)
    {
        //
        var panel = GetPanel(panelType);
        if (panel == null)
            return;
        //
        if (panel.pIsShow == true)
        {            
            panel.gameObject.SetActive(false);
            panel.OnHidePanel();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateTicks()
    {
        foreach (var panel in _dicPanels.Values)
        {
            if (panel.pIsShow == true)
            {
                panel.Tick();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateTicks_Sec()
    {
        foreach (var panel in _dicPanels.Values)
        {
            if (panel.pIsShow == true)
            {
                panel.Tick_Sec();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetLanguage(ELanguage language)
    {
        //
        _language = language;

        //
        foreach (var item in _dicPanels)
        {
            item.Value.Refresh();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ELanguage GetLanguage()
    {
        return _language;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextCommon(int key)
    {
        var temp = _900_CommonText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean  : return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default                : return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextSystem(int key)
    {
        var temp = _999_SystemText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextCharacter(int key)
    {
        var temp = _901_CharacterText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextSkill(int key)
    {
        var temp = _905_SkillsText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextNotice(int key)
    {
        var temp = _902_NoticeText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextMissions(int key)
    {
        var temp = _903_MissionsText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextItem(int key)
    {
        var temp = _904_ItemsText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextArmor(int key)
    {
        var temp = _906_ArmorsText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTextWeapon(int key)
    {
        var temp = _907_WeaponsText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat"></param>
    /// <returns></returns>
    public string GetTextCharacterStats(ECharacterStats stat)
    {
        //
        var result = string.Empty;

        //
        switch (stat)
        {
            case ECharacterStats.HP           : result = GetTextCommon(9000020); break;
            case ECharacterStats.HP_Level     : result = GetTextCommon(9000021); break;
            case ECharacterStats.ATK          : result = GetTextCommon(9000022); break;
            case ECharacterStats.ATK_Level    : result = GetTextCommon(9000023); break;
            case ECharacterStats.DEF          : result = GetTextCommon(9000024); break;
            case ECharacterStats.DEF_Level    : result = GetTextCommon(9000025); break;
            case ECharacterStats.Avoid        : result = GetTextCommon(9000026); break;
            case ECharacterStats.Avoid_Level  : result = GetTextCommon(9000027); break;
            case ECharacterStats.Focus        : result = GetTextCommon(9000028); break;
            case ECharacterStats.Focus_level  : result = GetTextCommon(9000029); break;
            case ECharacterStats.AtkSpd       : result = GetTextCommon(9000030); break;
            case ECharacterStats.AtkSpd_level : result = GetTextCommon(9000031); break;
            case ECharacterStats.Speed        : result = GetTextCommon(9000032); break;
            case ECharacterStats.Crirate      : result = GetTextCommon(9000033); break;
            case ECharacterStats.Crirate_level: result = GetTextCommon(9000034); break;
            case ECharacterStats.Cridmg       : result = GetTextCommon(9000035); break;
            case ECharacterStats.Cridmg_level : result = GetTextCommon(9000036); break;
            case ECharacterStats.ActiveSkill  : result = GetTextCommon(9000037); break;
            case ECharacterStats.PassiveSkill : result = GetTextCommon(9000038); break;
        }

        //
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetNotice(int key)
    {
        var temp = _902_NoticeText.GetItem(key);
        if (temp == null)
            return string.Empty;

        switch (_language)
        {
            case ELanguage.Korean: return temp.korean;
            case ELanguage.Japanese: return temp.japanese;
            default: return temp.english;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="colorCode"></param>
    /// <returns></returns>
    public Color GetColorHexaCode(string colorCode)
    {
        //
        if (ColorUtility.TryParseHtmlString(colorCode, out Color color))
        {
            return color;
        }

        //
        Debug.LogError("GetColorHexaCode() - Invalid color code: " + colorCode);

        return Color.white;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <returns></returns>
    public string GetSkillDesc(_103_CharacterSkills.Values tableInfo)
    {
        //
        var desc = GetTextSkill(tableInfo.description);
        desc = desc.Replace("$param0$", tableInfo.parameter0.ToString());
        desc = desc.Replace("$param1$", tableInfo.parameter1.ToString());
        desc = desc.Replace("$param2$", tableInfo.parameter2.ToString());
        desc = desc.Replace("$param3$", tableInfo.parameter3.ToString());
        desc = desc.Replace("$param4$", tableInfo.parameter4.ToString());
        desc = desc.Replace("$param5$", tableInfo.parameter5.ToString());
        desc = desc.Replace("$param6$", tableInfo.parameter6.ToString());
        desc = desc.Replace("$param7$", tableInfo.parameter7.ToString());
        desc = desc.Replace("$param8$", tableInfo.parameter8.ToString());
        desc = desc.Replace("$param9$", tableInfo.parameter9.ToString());

        //
        return desc;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="byteSize"></param>
    /// <returns></returns>
    public double GetFileSize(double byteSize)
    {
        return Math.Round(byteSize / (1024d * 1024d), 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageKey"></param>
    /// <param name="type"></param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void ShowMessageBox(string title, string message, Panel_MessageBox.EType type, Action onConfirm = null, Action onCancel = null)
    {
        var panel = ShowPanel(EPanelType.MessageBox) as Panel_MessageBox;
        panel.Init(title, message, type, onConfirm, onCancel);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowFlash(Action onComplete = null)
    {
        var panel = ShowPanel(EPanelType.Flash) as Panel_Flash;
        panel.Init();
        panel.Show(onComplete);
    }
}
