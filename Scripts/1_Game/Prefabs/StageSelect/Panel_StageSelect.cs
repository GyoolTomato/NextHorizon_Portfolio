using UnityEngine;

public class Panel_StageSelect : Panel_Slots<Com_StageSelect_Slot>
{
    //
    [SerializeField] Com_ContentsTitle _comTitle;
    [SerializeField] Com_StageSelect_StageDetail _comDetail;
    [SerializeField] Com_Button _btnConfirm;

    //
    DataStage _lastSelectedData;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.StageSelect;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        base.Init();

        //
        _comTitle.Init(OnBtnClose);

        //
        DeactiveSlots();

        foreach (var item in GD.pDataStage.pStageState)
        {
            var slot = ActivateSlot();
            slot.Init(item, OnBtnSlot);
        }

        //
        if (_lastSelectedData == null)
        {
            if (pSlots.Count > 0)
            {
                _lastSelectedData = pSlots[0].pData;
            }
        }

        if (_lastSelectedData != null) 
            OnBtnSlot(_lastSelectedData);

        //
        _btnConfirm.SetActive(_lastSelectedData != null);
        _btnConfirm.SetBtn(OnBtnConfirm);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    void OnBtnSlot(DataStage data)
    {
        //
        _lastSelectedData = data;

        //
        foreach (var item in pSlots)
        {
            item.SetSelected(item.pData.pTableInfo.key == data.pTableInfo.key);
        }

        //
        _comDetail.Init(_lastSelectedData);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnConfirm()
    {
        //
        GameManager.SetSelectedStage(_lastSelectedData.pTableInfo.stage, _lastSelectedData.pTableInfo.subNum);

        //
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Formation) as Panel_Formation;
        panel.Init();
    }
}