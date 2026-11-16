using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Com_StageSelect_Slot : Com_Base
{
    //
    [SerializeField] GameObject _bgSelected;
    [SerializeField] TextMeshProUGUI _stageNumber;
    [SerializeField] GameObject[] _starChecks;
    [SerializeField] TextMeshProUGUI _title;

    //
    Action<DataStage> _onBtn = null;

    //
    public DataStage pData { private set; get; } = null;


    /// <summary>
    /// 
    /// </summary>
    public void Init(DataStage data, Action<DataStage> onBtn)
    {
        //
        base.Init();

        //
        pData = data;
        _onBtn = onBtn;

        //
        _stageNumber.text = string.Format("{0}-{1}", pData.pTableInfo.stage.ToString().Replace("_", ""), pData.pTableInfo.subNum);
        _title.text = Manager_UI.Instance.GetTextStage(pData.pTableInfo.title);

        //
        if (_starChecks.Length >= 3)
            _starChecks[2].SetActive(pData.pIsCond_3);
        if (_starChecks.Length >= 2)
            _starChecks[1].SetActive(pData.pIsCond_2);
        if (_starChecks.Length >= 1)
            _starChecks[0].SetActive(pData.pIsCond_1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        _bgSelected.SetActive(isSelected);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtn()
    {
        _onBtn?.Invoke(pData);
    }
}
