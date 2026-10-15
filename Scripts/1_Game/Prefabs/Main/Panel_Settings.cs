using Observer;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : Panel_Base
{
    //
    [Header("Language")]
    [SerializeField] GameObject _langKoreanCheck;
    [SerializeField] GameObject _langEnglishCheck;
    [SerializeField] GameObject _langJapaneseCheck;

    //
    [Header("Account")]
    [SerializeField] Com_Button _comLogOut;


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

        //
        _comLogOut.SetActive(true);
        _comLogOut.SetBtn(() =>
        {
            Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextSystem(9990012), Manager_UI.Instance.GetTextSystem(9990013), Panel_MessageBox.EType.ConfirmCancel, () =>
            {                
                Manager_Account.Instance.LogOut();
                GameManager.ChangeLogoScene();
            });
        });
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
