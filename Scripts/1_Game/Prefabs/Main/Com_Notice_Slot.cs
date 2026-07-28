using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Com_Notice_Slot : Com_Base
{
    //
    [SerializeField] GameObject      _bgSelected = null;
    [SerializeField] TextMeshProUGUI _title = null;

    _800_Notice.Values _tableData = null;
    Action<_800_Notice.Values> _onBtn = null;

    /// <summary>
    /// 
    /// </summary>
    public void Init(int key, Action<_800_Notice.Values> onBtn)
    {
        _tableData = _800_Notice.GetItem(key);
        _onBtn = onBtn;

        _800_Notice.Values a = new _800_Notice.Values(1, "", "", 1, 2, 3);
        
        _title.text = Manager_UI.Instance.GetTextNotice(_tableData.title);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetKey()
    {
        return _tableData.key;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelected(bool isSelected)
    {
        _bgSelected.SetActive(isSelected);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        _onBtn?.Invoke(_tableData);
    }
}
