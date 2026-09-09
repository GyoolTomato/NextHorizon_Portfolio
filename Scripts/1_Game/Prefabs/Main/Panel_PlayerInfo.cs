using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Serialization;

public class Panel_PlayerInfo : Panel_Base,
    Observer.IObserver<Observer.IntroductionChangedEvent>,
    Observer.IObserver<Observer.PortraitChangedEvent>
{
    //
    [Header("Basic Infos")]
    [SerializeField] Image _portrait;
    [SerializeField] TextMeshProUGUI _level;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _uid;

    //
    [SerializeField] RectTransform _playerExpSlider;
    [SerializeField] TextMeshProUGUI _playerExp;

    //
    [SerializeField] Com_Button _btnEdit;
    [FormerlySerializedAs("_bioEdit"), SerializeField] TMP_InputField _introductionEdit;
    [FormerlySerializedAs("_bio"), SerializeField] GameObject _introduction;
    [FormerlySerializedAs("_bioText"), SerializeField] TextMeshProUGUI _introductionText;

    //
    [Header("Detail Infos")]
    [SerializeField] Com_PlayerInfo_Detail_Slot _comCreationDate;
    [SerializeField] Com_PlayerInfo_Detail_Slot _comBestStage;
    [SerializeField] Com_PlayerInfo_Detail_Slot _comCollectCount_Character;

    //
    GameData_PlayerInfo GD_playerInfo;

    bool _isEditMode;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.PlayerInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void OnEvent(Observer.IntroductionChangedEvent data)
    {
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void OnEvent(Observer.PortraitChangedEvent data)
    {
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        Observer.ObserverTracker<Observer.IntroductionChangedEvent>.Instance.Subscribe(this);
        Observer.ObserverTracker<Observer.PortraitChangedEvent>.Instance.Subscribe(this);

        //
        base.Init();        
        GD_playerInfo ??= GD.pPlayerInfo;

        //        
        _isEditMode = false;
        _btnEdit.SetActive(true);
        _btnEdit.SetBtn(() =>
        {
            _isEditMode = !_isEditMode;

            _btnEdit.SetText(Manager_UI.Instance.GetTextCommon(_isEditMode ? 9000001 : 9000066));

            _introductionEdit.gameObject.SetActive(_isEditMode);
            _introduction.gameObject.SetActive(_isEditMode == false);

            if (_isEditMode)
            {
                _introductionEdit.text = _introductionText.text;
            }
            else
            {
                _introductionText.text = string.Empty;
                ServerAPI.Instance.Send_ChangeIntroduction(_introductionEdit.text, null, null);                
            }
        });

        // Detail Infos
        //
        _comCreationDate.Init(Manager_UI.Instance.GetTextCommon(9000064), GD_playerInfo.pCreatedAt);

        //
        Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Refresh()
    {
        // Basic Infos
        //
        _portrait.sprite = Manager_Resources.Instance.GetCharacterSprite(ECharacterImageType.Portrait, GD_playerInfo.pPortrait);
        _level.text = string.Format(Manager_UI.Instance.GetTextCommon(9000039), GD_playerInfo.pLevel);
        _name.text = GD_playerInfo.pUserNickName;
        _uid.text = string.Format(Manager_UI.Instance.GetTextCommon(9000067), GD_playerInfo.pUid);

        //
        _playerExpSlider.localScale = new Vector3(
            Convert.ToSingle(GD.pPlayerInfo.pExp) / Manager_Table.Instance.GetPlayerLevelInfo(GD.pPlayerInfo.pLevel).expToNextLevel,
            1f, 1f);
        _playerExp.text = string.Format("{0} / {1}", GD.pPlayerInfo.pExp, Manager_Table.Instance.GetPlayerLevelInfo(GD.pPlayerInfo.pLevel).expToNextLevel);

        //
        _introductionText.text = GD_playerInfo.pIntroduction;
        _btnEdit.SetText(Manager_UI.Instance.GetTextCommon(_isEditMode ? 9000001 : 9000066));

        // Detail Infos
        //
        _comBestStage.Init(Manager_UI.Instance.GetTextCommon(9000062), "-");

        var activeCount = 0;
        foreach (var item in GD.pDataCharacter.pCharacters)
        {
            if (item.pIsActive)
            {
                activeCount++;
            }
        }
        _comCollectCount_Character.Init(Manager_UI.Instance.GetTextCommon(9000063), activeCount);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Hide()
    {
        //
        Observer.ObserverTracker<Observer.IntroductionChangedEvent>.Instance.Unsubscribe(this);
        Observer.ObserverTracker<Observer.PortraitChangedEvent>.Instance.Unsubscribe(this);

        //
        base.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnPortrait()
    {
        _102_Character.Values modelTableInfo = null;
        foreach (var item in _102_Character.GetList())
        {
            if (item.model == GD_playerInfo.pPortrait)
            {
                modelTableInfo = item;
                break;
            }
        }        

        var panel = Manager_UI.Instance.ShowPanel(EPanelType.CharacterSelect) as Panel_CharacterSelect;
        panel.Init(modelTableInfo, (tableInfo) =>
        {
            ServerAPI.Instance.Send_ChangePortrait(tableInfo.model, null, null);
        });
    }
}
