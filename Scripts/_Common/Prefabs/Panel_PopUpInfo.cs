using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpInfo : Panel_Base
{
    //
    [SerializeField] RectTransform _container;
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _descriptionText;
    [SerializeField] GameObject _lineForValue;
    [SerializeField] GameObject _valuesConatainer;
    [SerializeField] TextMeshProUGUI _mainValue;
    [SerializeField] TextMeshProUGUI _subValue_0;
    [SerializeField] TextMeshProUGUI _subValue_1;


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
    public void Init(Vector3 position, string title, string description, string titleColor = null,
        ECharacterStats mainStat = ECharacterStats.None, decimal mainValue = 0m,
        ECharacterStats subStat_0 = ECharacterStats.None, decimal subValue_0 = 0m,
        ECharacterStats subStat_1 = ECharacterStats.None, decimal subValue_1 = 0m)
    {
        //
        _titleText.color = string.IsNullOrEmpty(titleColor) ? Color.white : Manager_UI.Instance.GetColorHexaCode(titleColor);
        _titleText.text = string.Empty;
        _descriptionText.text = string.Empty;

        var isFoundValues = (mainStat != ECharacterStats.None && mainValue > 0m) || (subStat_0 != ECharacterStats.None && subValue_0 > 0m) || (subStat_1 != ECharacterStats.None && subValue_1 > 0m);
        _lineForValue.SetActive(isFoundValues);
        _valuesConatainer.SetActive(isFoundValues);
        _mainValue.gameObject.SetActive(mainStat != ECharacterStats.None && mainValue > 0m);
        _mainValue.text = string.Format("{0} {1}", Manager_UI.Instance.GetTextCharacterStats(mainStat), mainValue);
        _subValue_0.gameObject.SetActive(subStat_0 != ECharacterStats.None && subValue_0 > 0m);
        _subValue_0.text = string.Format("{0} {1}", Manager_UI.Instance.GetTextCharacterStats(subStat_0), subValue_0);
        _subValue_1.gameObject.SetActive(subStat_1 != ECharacterStats.None && subValue_1 > 0m);
        _subValue_1.text = string.Format("{0} {1}", Manager_UI.Instance.GetTextCharacterStats(subStat_1), subValue_1);

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
