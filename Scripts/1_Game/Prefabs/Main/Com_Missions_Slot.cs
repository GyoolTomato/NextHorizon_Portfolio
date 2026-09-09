using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Com_Missions_Slot : Com_Slots<Com_Item_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _title = null;

    [SerializeField] Button _btnConfirm = null;
    [SerializeField] Image  _state = null;
    [SerializeField] TextMeshProUGUI _txtState = null;

    [SerializeField] GameObject _dimClaimed = null;

    //
    DataMission _data = null;


    /// <summary>
    /// 
    /// </summary>
    public void Init(DataMission data)
    {
        //
        _data = data;

        //
        DeactiveSlots();

        //
        _title.text = string.Format(Manager_UI.Instance.GetTextMissions(_data.pTableInfo.title), _data.pTableInfo.count);

        //
        var slotExp = ActivateSlot();
        slotExp.Init(new DataItem(_data.pTableInfo.exp, _101_Items.GetItem(1010003)), EItemValueType.Name);

        //
        for (int i = 0; i < _data.pTableInfo.rewardKeys.Length; i++)
        {
            var item = _101_Items.GetItem(_data.pTableInfo.rewardKeys[i]);
            if (item != null)
            {
                var temp = new DataItem(_data.pTableInfo.rewardCounts[i], item);

                var slot = ActivateSlot();
                slot.Init(temp, EItemValueType.Name);
            }
        }

        //
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        var isAbleClick = false;
        var stateColor = Manager_UI.Instance.GetColorHexaCode("#00BFFF");
        if (_data.IsMissionCompleted())
        {
            isAbleClick = !_data.pIsClaimed;
            stateColor = _data.pIsClaimed ? Manager_UI.Instance.GetColorHexaCode("#666666") : Manager_UI.Instance.GetColorHexaCode("#228B22");
        }
        else
        {
            isAbleClick = false;
            //stateColor = Manager_UI.Instance.GetColorHexaCode("#DC3132");

        }
        _btnConfirm.interactable = isAbleClick;
        _state.color = stateColor;
        _txtState.text = string.Format("{0} / {1}", _data.pProgress, _data.pTableInfo.count);

        _dimClaimed.SetActive(_data.pIsClaimed);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnConfirm()
    {
        if (_data.pIsClaimed)
        {
            return;
        }
        else
        {
            if (_data.IsMissionCompleted())
            {
                ServerAPI.Instance.Send_MissionClaim(_data.pTableInfo.key, (success) =>
                {

                }, (error) =>
                {
                    Manager_UI.Instance.ShowMessageBox("", "", Panel_MessageBox.EType.OK);
                });
            }
        }
    }
}
