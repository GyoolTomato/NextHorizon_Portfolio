using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubPanel_CharacterInfo_LevelUp : Com_Base
{
    //
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] TextMeshProUGUI _exp;
    [SerializeField] RectTransform _expGauge;
    [SerializeField] Com_Item_Use_Slot _comUseBtn_1;
    [SerializeField] Com_Item_Use_Slot _comUseBtn_10;
    [SerializeField] Com_Item_Use_Slot _comUseBtn_100;

    //
    Character _character;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(Character character)
    {
        //
        _character = character;

        //
        var levelTableInfo = Manager_Table.Instance.GetCharacterLevelInfo(_character.pLevel);
        if (levelTableInfo == null)
        {
            Debug.LogError($"Level Table Info is null for level {_character.pLevel}");
            return;
        }

        //
        var isMaxLevel = _character.pLevel >= _100_CommonValues.GetItem(1000001).value;
        var gaugeScale = isMaxLevel ? 1 : _character.pExp / levelTableInfo.expToNextLevel;
        if (gaugeScale > 1)
            gaugeScale = 1;

        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), _character.pLevel);
        _exp.text = isMaxLevel ? "MAX" : string.Format("{0} / {1}", _character.pExp, levelTableInfo.expToNextLevel);
        _expGauge.localScale = new Vector3(gaugeScale, 1, 1);

        //
        var tableInfo = _101_Items.GetItem(1010003);
        if (tableInfo == null)
        {
            Debug.LogError($"Item Table Info is null for item {1010003}");
            return;
        }

        //
        _comUseBtn_1.Init(tableInfo, 1, true, OnBtn_1);
        _comUseBtn_10.Init(tableInfo, 10, true, OnBtn_10);
        _comUseBtn_100.Init(tableInfo, 100, true, OnBtn_100);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    void OnBtn_1(_101_Items.Values tableInfo)
    {
        if (GlobalData.Instance.pDataPlayerInfo.RemoveItemCount(EItemType.ExpCard, 1))
        {
            _comUseBtn_1.Refresh();
            _comUseBtn_10.Refresh();
            _comUseBtn_100.Refresh();
            Manager_Character.Instance.DoAddExp(_character.pTableInfo.key, 10);
        }
        else
        {
            Debug.LogError($"Not enough item {tableInfo.key} to use.");
        }    
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    void OnBtn_10(_101_Items.Values tableInfo)
    {
        if (GlobalData.Instance.pDataPlayerInfo.RemoveItemCount(EItemType.ExpCard, 10))
        {
            _comUseBtn_1.Refresh();
            _comUseBtn_10.Refresh();
            _comUseBtn_100.Refresh();
            Manager_Character.Instance.DoAddExp(_character.pTableInfo.key, 100);
        }
        else
        {
            Debug.LogError($"Not enough item {tableInfo.key} to use.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    void OnBtn_100(_101_Items.Values tableInfo)
    {
        if (GlobalData.Instance.pDataPlayerInfo.RemoveItemCount(EItemType.ExpCard, 100))
        {
            _comUseBtn_1.Refresh();
            _comUseBtn_10.Refresh();
            _comUseBtn_100.Refresh();
            Manager_Character.Instance.DoAddExp(_character.pTableInfo.key, 1000);
        }
        else
        {
            Debug.LogError($"Not enough item {tableInfo.key} to use.");
        }
    }
}
