using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Com_Missions_Slot : Com_Slots<Com_Item_Slot>
{
    //
    [SerializeField] TextMeshProUGUI _title = null;

    [SerializeField] Button _btnConfirm = null;
    [SerializeField] Image  _btnImage = null;
    [SerializeField] TextMeshProUGUI _btnText = null;

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
        var isAbleClick = false;
        var btnColor = Color.white;
        var btnTextKey = 0;
        if (data.IsMissionCompleted())
        {
            isAbleClick = !data.pIsTake;
            btnColor = data.pIsTake ? Manager_UI.Instance.GetColorHexaCode("#666666") : Manager_UI.Instance.GetColorHexaCode("#228B22");
            btnTextKey = data.pIsTake ? 9000043 : 9000041;
        }
        else
        {
            isAbleClick = false;
            btnColor = Manager_UI.Instance.GetColorHexaCode("#DC3132");
            btnTextKey = 9000042;
            
        }
        _btnConfirm.interactable = isAbleClick;
        _btnImage.color = btnColor;
        _btnText.text = Manager_UI.Instance.GetTextCommon(btnTextKey);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnConfirm()
    {
        
    }
}
