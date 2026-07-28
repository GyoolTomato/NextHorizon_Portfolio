using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextSupport : MonoBehaviour
{
    //
    [SerializeField] bool _isSystem;
    [SerializeField] int _key = 0;

    //
    ELanguage _appliedLanguage = ELanguage.None;
    int _appliedKey = 0;
    TextMeshProUGUI _text = null;


    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        ChangeText();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        ChangeText();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeText()
    {
        //
        if (_text == null)
            return;

        //
        if (_key == 0 || _appliedLanguage != Manager_UI.Instance.GetLanguage() || _appliedKey != _key)
        {
            _appliedLanguage = Manager_UI.Instance.GetLanguage();
            _appliedKey = _key;
            _text.text = _isSystem ? Manager_UI.Instance.GetTextSystem(_key) : Manager_UI.Instance.GetTextCommon(_key);
        }
    }
}
