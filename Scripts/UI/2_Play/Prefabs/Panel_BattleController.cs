using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_BattleController : Panel_Base
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _remainTime;
    [SerializeField] RectTransform _barHP;
    [SerializeField] TextMeshProUGUI _valueHP;
    [SerializeField] Button _btnSetting;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _activeSkill;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _passiveSkill_0;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _passiveSkill_1;
    [SerializeField] FloatingJoystick _joyStick;

    Character _player;

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.BattleController;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Init()
    {
        //
        base.Init();

        //
        _title.text = Manager_UI.Instance.GetTextStage(GD.pDataInGame.pStageInfo.pTableInfo.title);
        _remainTime.text = "00:00";

        //
        _player = GameData.Instance.pDataInGame.pPlayer;

        //
        _barHP.localScale = Vector3.one;
        _valueHP.text = Manager_Character.Instance.GetTotalStat(_player, ECharacterStats.HP).ToString();

        //
        _activeSkill   .Init(GameData.Instance.pDataInGame.pDataPlayer.pData.pTableInfo.activeSkill);
        _passiveSkill_0.Init(GameData.Instance.pDataInGame.pDataPlayer.pData.pTableInfo.passiveSkill_0);
        _passiveSkill_1.Init(GameData.Instance.pDataInGame.pDataPlayer.pData.pTableInfo.passiveSkill_1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnSettings()
    {
        PlayScene.Pause(true);

        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Settings) as Panel_Settings;
        panel.Init();
        panel.SetOnHide(() =>
        {
            PlayScene.Pause(false);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Tick()
    {
        //
        if (_player == null || Manager_Game.pIsStopGameTimer)
        {
            return;
        }

        //
        int minutes = Mathf.FloorToInt(GameData.Instance.pDataInGame.pRemainTime / 60f);
        int seconds = Mathf.FloorToInt(GameData.Instance.pDataInGame.pRemainTime % 60f);
        _remainTime.text = $"{minutes:00}:{seconds:00}";

        //
        var hpScale = _player.pComStats.pHp / Convert.ToSingle(_player.pComStats.pHpMax);
        _barHP.localScale = new Vector3(hpScale < 0 ? 0 : hpScale, 1, 1);
        _valueHP.text = (_player.pComStats.pHp < 0 ? 0 : _player.pComStats.pHp).ToString();

        //
        var direction = Vector3.forward * _joyStick.Vertical + Vector3.right * _joyStick.Horizontal;
       _player.transform.position += direction * _player.pComStats.pSpeed * Manager_Game.pGameSpeed * Time.deltaTime;

        if (_joyStick.Vertical == 0f && _joyStick.Horizontal == 0f)
        {
            if (_player.pCurState.pState == ECharacterState.Running)
            {
                _player.ChangeState(ECharacterState.Idle);
            }            
        }
        else
        {
            _player.pComAnim.SetDirection(direction.x);
            _player.ChangeState(ECharacterState.Running);
        }
    }
}
