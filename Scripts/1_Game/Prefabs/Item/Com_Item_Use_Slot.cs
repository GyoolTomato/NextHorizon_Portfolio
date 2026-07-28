using System;
using TMPro;
using UnityEngine;

public class Com_Item_Use_Slot : Com_Base
{
    //
    [SerializeField] Com_Item_Slot _comItemSlot;
    [SerializeField] TextMeshProUGUI _txtValue;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="curTbleInfo"></param>
    public void Init(_101_Items.Values curTbleInfo, long value, bool isMinus, Action<_101_Items.Values> onBtn = null)
    {
        //
        _comItemSlot.Init(curTbleInfo, GlobalData.Instance.pDataPlayerInfo.GetItemCount(curTbleInfo.type), onBtn);

        SetValue(value, isMinus);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="armTableInfo"></param>
    public void Init(_105_Armors.Values armTableInfo, long value, bool isMinus, Action<_105_Armors.Values> onBtn = null)
    {
        //
        _comItemSlot.Init(armTableInfo, onBtn);

        SetValue(value, isMinus);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wepTableInfo"></param>
    public void Init(_106_Weapons.Values wepTableInfo, long value, bool isMinus, Action<_106_Weapons.Values> onBtn = null)
    {
        //
        _comItemSlot.Init(wepTableInfo, onBtn);

        SetValue(value, isMinus);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Refresh()
    {
        _comItemSlot.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isMinus"></param>
    void SetValue(long value, bool isMinus)
    {
        _txtValue.color = isMinus ? Color.red : Color.green;
        _txtValue.text = isMinus ? $"-{value}" : $"+{value}";
    }
}
