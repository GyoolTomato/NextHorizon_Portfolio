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
    [SerializeField] GameObject _frame_Normal = null;
    [SerializeField] GameObject _frame_Rare = null;
    [SerializeField] GameObject _frame_Elite = null;
    [SerializeField] GameObject _frame_Epic = null;
    [SerializeField] GameObject _frame_Legend = null;

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
        _frame_Normal.SetActive(pData.pTableInfo.grade == EGrade.Normal);
        _frame_Rare.SetActive(pData.pTableInfo.grade == EGrade.Rare);
        _frame_Elite.SetActive(pData.pTableInfo.grade == EGrade.Elite);
        _frame_Epic.SetActive(pData.pTableInfo.grade == EGrade.Epic);
        _frame_Legend.SetActive(pData.pTableInfo.grade == EGrade.Legend);

        //
        _icon.sprite = Manager_Resources.Instance.GetIconSprite(pData.pTableInfo.key);

        //
        Refresh();     
    }

    /// <summary>
    /// 
    /// </summary>
    public void Refresh()
    {
        //
        if (_quantity != null)
            _quantity.text = pData.pCount.ToString();

        if (_value != null)
            _value.text = Manager_Inventory.Instance.GetValueText(pData, _type);
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
