using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class Panel_BattleResult : Panel_Slots<Com_Reward_Slot>
{
    //    
    [SerializeField] GameObject _bgWin;
    [SerializeField] GameObject _bgLose;
    [SerializeField] GameObject _titleWin;
    [SerializeField] GameObject _titleLose;

    //
    [SerializeField] GameObject _starCheck0;
    [SerializeField] GameObject _starCheck1;
    [SerializeField] GameObject _starCheck2;

    //
    [SerializeField] GameObject _rewardsEmpty;

    //
    [SerializeField] Com_Button _btnClose;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.BattleResult;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerPlayerWeaponData[] weapons, ServerPlayerArmorData[] armors, ServerMissionRewardData[] rewards)
    {
        //
        base.Init();

        //
        DeactiveSlots();

        //
        var isWin = Manager_Playing.Instance.pBattleResult;

        //
        _bgWin.SetActive(isWin > 0);
        _bgLose.SetActive(isWin <= 0);
        _titleWin.SetActive(isWin > 0);
        _titleLose.SetActive(isWin <= 0);

        //
        _starCheck0.SetActive(isWin > 0);
        _starCheck1.SetActive(isWin > 1);
        _starCheck2.SetActive(isWin > 2);

        //
        _rewardsEmpty.SetActive(isWin <= 0);
        _container.gameObject.SetActive(isWin > 0);
        if (_container.gameObject.activeSelf)
            SetRewards(weapons, armors, rewards);

        //
        _btnClose.SetActive(true);
        _btnClose.SetText(Manager_UI.Instance.GetTextCommon(9000001));
        _btnClose.SetBtn(() =>
        {
            OnBtnClose();

            Manager_Game.ChangeScene(ESceneType.Game);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void SetRewards(ServerPlayerWeaponData[] weapons, ServerPlayerArmorData[] armors, ServerMissionRewardData[] rewards)
    {   
        //
        foreach (var item in weapons)
        {
            //
            var tableInfo = _106_Weapons.GetItem(item.weaponKey);
            if (tableInfo == null)
            {
                continue;
            }

            //
            var weaponData = new DataWeapon(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);
            
            var slot = ActivateSlot();
            slot.Init(weaponData, EItemValueType.Name);
        }

        //
        foreach (var item in armors)
        {
            //
            var tableInfo = _105_Armors.GetItem(item.armorKey);
            if (tableInfo == null)
            {
                continue;
            }

            //
            var weaponData = new DataArmor(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);

            var slot = ActivateSlot();
            slot.Init(weaponData, EItemValueType.Name);
        }

        //
        foreach (var item in rewards)
        {
            //
            var tableInfo = _101_Items.GetItem(item.itemKey);
            if (tableInfo == null)
            {
                continue;
            }

            //
            var slot = ActivateSlot();
            slot.Init(tableInfo, EItemValueType.Name, item.quantity);
        }
    }
}
