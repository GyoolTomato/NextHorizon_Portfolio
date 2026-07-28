using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpInfo : Panel_Base
{
    //
    [SerializeField] RectTransform _container;
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _descriptionText;


    //
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.PopUpInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(Vector3 position, string title, string description, string titleColor = null)
    {
        //
        _titleText.color = string.IsNullOrEmpty(titleColor) ? Color.white : Manager_UI.Instance.GetColorHexaCode(titleColor);
        _titleText.text = string.Empty;
        _descriptionText.text = string.Empty;

        //
        var screenPoint = Camera.main.WorldToScreenPoint(position);
        var parentRectTransform = _container.parent as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, Camera.main, out Vector2 localPoint);
        
        if (localPoint.x + _container.sizeDelta.x > parentRectTransform.rect.size.x / 2)
            localPoint.x = localPoint.x - _container.sizeDelta.x;
        if (localPoint.y - _container.sizeDelta.y < -parentRectTransform.rect.size.y / 2)
            localPoint.y = localPoint.y + _container.sizeDelta.y;

        _container.localPosition = localPoint;

        //
        _titleText.text = title;
        _descriptionText.text = description;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnHidePanel()
    {
        //
        base.OnHidePanel();

        //
        _titleText.text = string.Empty;
        _descriptionText.text = string.Empty;
    }
}
