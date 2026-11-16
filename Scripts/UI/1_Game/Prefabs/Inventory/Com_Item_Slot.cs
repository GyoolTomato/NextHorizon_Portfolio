using System;
using System.Xml.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Com_Item_Slot : Com_Base, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //
    [SerializeField] Image _frame = null;
    [SerializeField] Image _icon = null;

    [SerializeField] TextMeshProUGUI _quantity = null;
    [SerializeField] TextMeshProUGUI _value = null;

    //
    EItemValueType _type = EItemValueType.None;
    Action<Com_Item_Slot> _onPointerDown = null;
    Action<Com_Item_Slot> _onPointerPressing = null;
    Action<Com_Item_Slot> _onPointerUp = null;
    
    bool _isPressed = false;    

    //
    public DataItem pData { private set; get; }
    public float pPressingTime { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    public void Init(DataItem data, EItemValueType type)
    {
        //
        _isPressed = false;
        pPressingTime = 0f;        

        //
        pData = data;
        _type = type;

        //
        _frame.color = Manager_UI.Instance.GetGradeColorHexaCode(pData != null ? pData.pTableInfo.grade : EGrade.Normal);
        _icon.gameObject.SetActive(pData != null);
        _icon.sprite = pData != null ? Manager_Resources.Instance.GetIconSprite(pData.pTableInfo.key) : null;

        //
        Refresh();     
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        if (_quantity != null)
            _quantity.text = pData != null ? Manager_UI.Instance.BigNumberNotation(pData.pCount) : string.Empty;

        if (_value != null)
            _value.text = pData != null ? Manager_Inventory.Instance.GetValueText(pData, _type) : string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onPointerDown"></param>
    /// <param name="onPointerPressing"></param>
    /// <param name="onPointerUp"></param>
    public void SetPointerControl(Action<Com_Item_Slot> onPointerDown, Action<Com_Item_Slot> onPointerPressing, Action<Com_Item_Slot> onPointerUp)
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
            panel.Init(transform.position, Manager_UI.Instance.GetTextItem(pData.pTableInfo.name), Manager_UI.Instance.GetTextItem(pData.pTableInfo.desc));
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
