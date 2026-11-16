using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMultiImage : Button
{
    //
    List<Image> _images = null;
    List<Text> _texts = null;
    List<TextMeshProUGUI> _textMeshPros = null;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {   
        //
        base.Awake();

        //
        onClick.AddListener(OnClickForEffect);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnEnable()
    {
        //
        base.OnEnable();

        //

    }
 
    /// <summary>
    /// 
    /// </summary>
    void OnClickForEffect()
    {
        if (_images != null)
        {
            foreach (var image in _images)
            {
                
            }
        }

        if (_texts != null)
        {
            foreach (var text in _texts)
            {

            }
        }

        if (_textMeshPros != null)
        {
            foreach (var text in _textMeshPros)
            {

            }
        }
    }
}
