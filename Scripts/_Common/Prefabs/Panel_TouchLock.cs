using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Panel_TouchLock : Panel_Base
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
        pPanelType = EPanelType.TouchLock;

        //
        _dim.color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isShowDim"></param>
    public void Init(bool isActiveDim)
    {
        //
        base.Init();

        //
        _sequence?.Kill();

        //
        _dim.color = new Color(1f, 1f, 1f, isActiveDim ? 1f : 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isShowAnim"></param>
    public void Hide(bool isShowAnim)
    {
        if (isShowAnim)
        {
            _sequence = DOTween.Sequence().Append(_dim.DOFade(0f, 0.2f)).OnComplete(() =>
            {
                base.Hide();
            });
        }
        else
        {
            base.Hide();
        }
    }
}
