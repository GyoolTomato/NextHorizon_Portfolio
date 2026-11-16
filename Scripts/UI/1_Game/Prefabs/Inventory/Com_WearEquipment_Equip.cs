using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_WearEquipment_Equip : Com_Base
{
    //
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] Com_Button _btnLevelUp;
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] TextMeshProUGUI _part;
    [SerializeField] TextMeshProUGUI _grade;
    [SerializeField] TextMeshProUGUI _mainValue;
    [SerializeField] TextMeshProUGUI _subValue_0;
    [SerializeField] TextMeshProUGUI _subValue_1;
    [SerializeField] TextMeshProUGUI _desc;
    [SerializeField] GameObject _emptyText;

    //
    DataWeapon _dataWeapon;
    DataArmor _dataArmor;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void Init(DataArmor data)
    {
        _dataArmor = data;
        _icon.gameObject.SetActive(data != null);
        _icon.sprite = data != null ? Manager_Resources.Instance.GetIconSprite(data.pTableInfo.key) : null;

        _name.text = data != null ? Manager_UI.Instance.GetTextArmor(data.pTableInfo.name) : string.Empty;
        _name.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;
        _btnLevelUp.gameObject.SetActive(data != null && Manager_Inventory.Instance.IsMaxLevel_Armor(data.pLevel) == false);
        _btnLevelUp.SetActive(true);
        _btnLevelUp.SetBtn(OnBtnLevelUp_Armor);

        _level.text = data != null ? Manager_Inventory.Instance.GetValueText(data, EItemValueType.Level) : string.Empty;
        _level.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;
        _part.gameObject.SetActive(true);
        _part.text = data != null ? data.pTableInfo.type.ToString() : string.Empty;
        _part.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;
        _grade.text = data != null ? Manager_Inventory.Instance.GetValueText(data, EItemValueType.Grade) : string.Empty;
        _grade.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;

        _mainValue.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pTableInfo.mainValueType), 
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Main)) : string.Empty;
        _subValue_0.gameObject.SetActive(data != null ? data.pSubValueType_0 != ECharacterStats.None && data.pTableInfo.subValue > 0d : false);
        _subValue_0.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pSubValueType_0), 
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Sub_0)) : string.Empty;
        _subValue_1.gameObject.SetActive(data != null ? data.pSubValueType_1 != ECharacterStats.None && data.pTableInfo.subValue > 0d : false);
        _subValue_1.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pSubValueType_1), 
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Sub_1)) : string.Empty;

        _desc.text = data != null ? Manager_UI.Instance.GetTextArmor(data.pTableInfo.desc) : string.Empty;

        _emptyText.SetActive(data == null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void Init(DataWeapon data)
    {
        _dataWeapon = data;
        _icon.gameObject.SetActive(data != null);
        _icon.sprite = data != null ? Manager_Resources.Instance.GetIconSprite(data.pTableInfo.key) : null;

        _name.text = data != null ? Manager_UI.Instance.GetTextWeapon(data.pTableInfo.name) : string.Empty;
        _name.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;
        _btnLevelUp.gameObject.SetActive(data != null && Manager_Inventory.Instance.IsMaxLevel_Weapon(data.pLevel) == false);
        _btnLevelUp.SetActive(true);
        _btnLevelUp.SetBtn(OnBtnLevelUp_Weapon);

        _level.text = data != null ? Manager_Inventory.Instance.GetValueText(data, EItemValueType.Level) : string.Empty;
        _level.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;
        _part.gameObject.SetActive(false);
        _grade.text = data != null ? Manager_Inventory.Instance.GetValueText(data, EItemValueType.Grade) : string.Empty;
        _grade.color = data != null ? Manager_UI.Instance.GetGradeColorHexaCode(data.pTableInfo.grade) : Color.white;

        _mainValue.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pTableInfo.mainValueType),
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Main)) : string.Empty;
        _subValue_0.gameObject.SetActive(data != null ? data.pSubValueType_0 != ECharacterStats.None && data.pTableInfo.subValue > 0d : false);
        _subValue_0.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pSubValueType_0),
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Sub_0)) : string.Empty;
        _subValue_1.gameObject.SetActive(data != null ? data.pSubValueType_1 != ECharacterStats.None && data.pTableInfo.subValue > 0d : false);
        _subValue_1.text = data != null ? string.Format("{0} {1}", 
            Manager_UI.Instance.GetTextCharacterStats(data.pSubValueType_1),
            Manager_Inventory.Instance.GetEquipStat(data, EEquipStatType.Sub_1)) : string.Empty;

        _desc.text = data != null ? Manager_UI.Instance.GetTextWeapon(data.pTableInfo.desc) : string.Empty;

        _emptyText.SetActive(data == null);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnLevelUp_Weapon()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.LevelUp) as Panel_LevelUp;
        panel.Init(_dataWeapon, () =>
        {
            Init(_dataWeapon);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    void OnBtnLevelUp_Armor()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.LevelUp) as Panel_LevelUp;
        panel.Init(_dataArmor, () =>
        {
            Init(_dataArmor);
        });
    }
}