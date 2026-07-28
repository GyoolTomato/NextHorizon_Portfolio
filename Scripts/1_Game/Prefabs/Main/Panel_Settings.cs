using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : Panel_Base
{
    //
    [SerializeField] GameObject _langKoreanCheck;
    [SerializeField] GameObject _langEnglishCheck;
    [SerializeField] GameObject _langJapaneseCheck;

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.Settings;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        var language = Manager_UI.Instance.GetLanguage();

        _langKoreanCheck  .SetActive(language == ELanguage.Korean  );
        _langEnglishCheck .SetActive(language == ELanguage.English );
        _langJapaneseCheck.SetActive(language == ELanguage.Japanese);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnHidePanel()
    {
        //
        if (_langKoreanCheck.activeSelf)
        {
            Manager_UI.Instance.SetLanguage(ELanguage.Korean);
        }
        else if (_langEnglishCheck.activeSelf)
        {
            Manager_UI.Instance.SetLanguage(ELanguage.English);
        }
        else if (_langJapaneseCheck.activeSelf)
        {
            Manager_UI.Instance.SetLanguage(ELanguage.Japanese);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClickLangKorean()
    {
        _langKoreanCheck  .SetActive(true );
        _langEnglishCheck .SetActive(false);
        _langJapaneseCheck.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClickLangEnglish()
    {
        _langKoreanCheck  .SetActive(false);
        _langEnglishCheck .SetActive(true );
        _langJapaneseCheck.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClickLangJapanese()
    {
        _langKoreanCheck  .SetActive(false);
        _langEnglishCheck .SetActive(false);
        _langJapaneseCheck.SetActive(true );
    }
}
