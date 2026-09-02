using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Panel_Flash : Panel_Base
{
    //
    [SerializeField] Image _dim;

    //
    Sequence _sequence;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();
                
        //
        pPanelType = EPanelType.Flash;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        _sequence?.Kill();

        _dim.color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Show(Action onComplete)
    {
        _sequence = DOTween.Sequence().Append(_dim.DOFade(1f, 0.05f)).Append(_dim.DOFade(0f, 0.2f)).OnComplete(() =>
        {
            onComplete?.Invoke();

            Hide();
        });
    }
}
