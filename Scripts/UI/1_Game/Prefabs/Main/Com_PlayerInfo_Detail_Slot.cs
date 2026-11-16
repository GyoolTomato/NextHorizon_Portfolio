using UnityEngine;
using TMPro;

public class Com_PlayerInfo_Detail_Slot : Com_Base
{
    //
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _value;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="value"></param>
    public void Init(string title, decimal value)
    {
        _title.text = title;
        _value.text = value.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="value"></param>
    public void Init(string title, string value)
    {
        _title.text = title;
        _value.text = value;
    }
}
