using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Panel_Notice : Panel_Slots<Com_Notice_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _content = null;

    //
    List<_800_Notice.Values> _list = null;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        pPanelType = EPanelType.Notice;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        _content.text = string.Empty;
        DeactiveSlots();

        Refresh();

        OnBtn(_list[0]);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        _list = _800_Notice.GetList();
        
        foreach (var data in _list)
        {
            var slot = ActivateSlot();
            slot.Init(data.key, OnBtn);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    void OnBtn(_800_Notice.Values data)
    {
        //
        foreach (var item in pSlots)
        {
            item.SetSelected(data.key == item.GetKey());
        }

        //
        _content.text = string.Empty;
        _content.text = Manager_UI.Instance.GetNotice(data.content);
    }
}