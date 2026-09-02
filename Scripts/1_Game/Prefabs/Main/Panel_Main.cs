using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Panel_Main : Panel_Base
{
    //
    [SerializeField] RectTransform _rtPlayerInfo = null;
    [SerializeField] RectTransform _rtTopLeft = null;
    [SerializeField] RectTransform _rtTopRight_Top = null;
    [SerializeField] RectTransform _rtBtnWorks = null;
    [SerializeField] RectTransform _rtMenu = null;

    [SerializeField] TextMeshProUGUI _playerLevel = null;
    [SerializeField] TextMeshProUGUI _playerName = null;
    [SerializeField] TextMeshProUGUI _playerExp = null;

    [SerializeField] Com_UserAssets _comUserAssets = null;

    [SerializeField] TextMeshProUGUI _realTime = null;

    //
    StringBuilder _sb = new StringBuilder();


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.Main;
    }


    private void OnDestroy()
    {
        Debug.LogWarning(
            $"[OnDestroy] {gameObject.name} destroyed!\n" +
            $"Scene: {gameObject.scene.name}\n" +
            $"Parent: {(transform.parent != null ? transform.parent.name : "None")}\n" +
            $"IsDontDestroyOnLoad: {gameObject.scene.name == "DontDestroyOnLoad\\"}"
    );
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        _playerLevel.text = GameData.Instance.pPlayerInfo.pLevel.ToString();
        _playerName.text = GameData.Instance.pPlayerInfo.pUserNickName;
        _playerExp.text = GameData.Instance.pPlayerInfo.pExp.ToString();

        //
        _comUserAssets.Init(EItemType.Crystal, EItemType.Gold);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        //
        var textSupports = transform.GetComponentsInChildren<TextSupport>();
        foreach (var item in textSupports)
        {
            item.ChangeText();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnPlayerInfo()
    {
        Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextCommon(9000045), Manager_UI.Instance.GetTextSystem(9990007), Panel_MessageBox.EType.OK, () =>
        {
            return;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnNotice()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Notice) as Panel_Notice;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnMissions()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Missions) as Panel_Missions;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLobbyCharacter()
    {
        Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextCommon(9000044), Manager_UI.Instance.GetTextSystem(9990007), Panel_MessageBox.EType.OK, () =>
        {
            return;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnSettings()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Settings) as Panel_Settings;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnChangeView()
    {
        //
        var isHide = _rtPlayerInfo.gameObject.activeSelf;

        //
        _rtPlayerInfo  .gameObject.SetActive(!isHide);
        _rtTopLeft     .gameObject.SetActive(!isHide);
        _rtBtnWorks    .gameObject.SetActive(!isHide);
        _rtMenu        .gameObject.SetActive(!isHide);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnStudents()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Characters) as Panel_Characters;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnFormation()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Formation) as Panel_Formation;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnInventory()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Inventory) as Panel_Inventory;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnShop()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Shop) as Panel_Shop;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnGacha()
    {
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Gacha) as Panel_Gacha;
        panel.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnWork()
    {
        Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextCommon(9000016), Manager_UI.Instance.GetTextSystem(9990007), Panel_MessageBox.EType.OK, () =>
        {
            return;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Tick_Sec()
    {
        _sb.Clear();
        _sb.Append(DateTime.Now.ToString("HH:mm:ss"));
        _realTime.text = _sb.ToString();
    }
}
