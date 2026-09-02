using System;
using System.Xml.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Com_Armor_Slot : Com_Base, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //
    [SerializeField] GameObject _frame_Normal = null;
    [SerializeField] GameObject _frame_Rare = null;
    [SerializeField] GameObject _frame_Elite = null;
    [SerializeField] GameObject _frame_Epic = null;
    [SerializeField] GameObject _frame_Legend = null;

    [SerializeField] Image _icon = null;

    [SerializeField] TextMeshProUGUI _part = null;
    [SerializeField] TextMeshProUGUI _level = null;
    [SerializeField] TextMeshProUGUI _value = null;

    //
    EItemValueType _type = EItemValueType.None;
    Action<Com_Armor_Slot> _onPointerDown = null;
    Action<Com_Armor_Slot> _onPointerPressing = null;
    Action<Com_Armor_Slot> _onPointerUp = null;

    bool _isPressed = false;

    //
    public DataArmor pData { private set; get; }
    public float pPressingTime { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataArmor data, EItemValueType type)
    {
        //
        _isPressed = false;
        pPressingTime = 0f;

        //
        pData = data;
        _type = type;

        //
        _frame_Normal.SetActive(pData.pTableInfo.grade == EGrade.Normal);
        _frame_Rare.SetActive(pData.pTableInfo.grade == EGrade.Rare);
        _frame_Elite.SetActive(pData.pTableInfo.grade == EGrade.Elite);
        _frame_Epic.SetActive(pData.pTableInfo.grade == EGrade.Epic);
        _frame_Legend.SetActive(pData.pTableInfo.grade == EGrade.Legend);

        //
        _icon.sprite = Manager_Resources.Instance.GetSprite(pData.pTableInfo.icon);

        //
        if (_part != null)
            _part.text = string.Empty;
        
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="value"></param>
    public void Init(_105_Armors.Values tableInfo, EItemValueType type)
    {
        //
        Init(new DataArmor(-1, 1, 0, 0, tableInfo), type);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Refresh()
    {
        //
        if (_level != null)
            _level.text = string.Format("+{0}", pData.pLevel);

        if (_value != null)
            _value.text = Manager_Inventory.Instance.GetValueText(pData, _type);        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onPointerDown"></param>
    /// <param name="onPointerPressing"></param>
    /// <param name="onPointerUp"></param>
    public void SetPointerControl(Action<Com_Armor_Slot> onPointerDown, Action<Com_Armor_Slot> onPointerPressing, Action<Com_Armor_Slot> onPointerUp)
    {
        _onPointerDown = onPointerDown;
        _onPointerPressing = onPointerPressing;
        _onPointerUp = onPointerUp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //
        pPressingTime = 0.0f;
        _isPressed = true;

        //
        _onPointerDown?.Invoke(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //
        pPressingTime = 0.0f;
        _isPressed = false;

        //
        if (_onPointerUp == null)
        {
            var panel = Manager_UI.Instance.ShowPanel(EPanelType.PopUpInfo) as Panel_PopUpInfo;
            panel.Init(transform.position, Manager_UI.Instance.GetTextArmor(pData.pTableInfo.name), Manager_UI.Instance.GetTextArmor(pData.pTableInfo.desc));
        }
        else
        {
            _onPointerUp?.Invoke(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //OnPointerUp(eventData);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Tick()
    {
        if (_isPressed)
        {
            _onPointerPressing?.Invoke(this);

            pPressingTime += Time.deltaTime;
        }
    }
}
