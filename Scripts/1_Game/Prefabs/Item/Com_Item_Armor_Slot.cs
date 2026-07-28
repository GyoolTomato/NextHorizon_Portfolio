using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_Item_Armor_Slot : Com_Base
{
    //
    [SerializeField] GameObject _frame_Normal = null;
    [SerializeField] GameObject _frame_Rare = null;
    [SerializeField] GameObject _frame_Elite = null;
    [SerializeField] GameObject _frame_Epic = null;
    [SerializeField] GameObject _frame_Legend = null;

    [SerializeField] Image _icon = null;

    [SerializeField] TextMeshProUGUI _part = null;
    [SerializeField] TextMeshProUGUI _amount = null;

    //
    public _105_Armors.Values pTableInfo { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="value"></param>
    public void Init(_105_Armors.Values tableInfo, long value)
    {
        //
        pTableInfo = tableInfo;

        //
        _frame_Normal.SetActive(tableInfo.grade == EGrade.Normal);
        _frame_Rare.SetActive(tableInfo.grade == EGrade.Rare);
        _frame_Elite.SetActive(tableInfo.grade == EGrade.Elite);
        _frame_Epic.SetActive(tableInfo.grade == EGrade.Epic);
        _frame_Legend.SetActive(tableInfo.grade == EGrade.Legend);

        //
        _icon.sprite = Manager_Resources.Instance.GetSprite(tableInfo.icon);

        //
        if (_part != null)
            _part.text = string.Empty;

        if (_amount != null)
            _amount.text = value.ToString();
    }
}
