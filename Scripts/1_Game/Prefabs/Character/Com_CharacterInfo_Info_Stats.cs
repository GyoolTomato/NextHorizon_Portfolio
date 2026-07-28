using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Com_CharacterInfo_Info_Stats : Com_Base
{
    //
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotHP      = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotATK     = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotDEF     = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotAvoid   = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotFocus   = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotAtkSpd  = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotSpeed   = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotCriRate = null;
    [SerializeField] Com_CharacterInfo_Info_Stats_Slot _slotCriDmg  = null;

    //
    Character _character = null;   


    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(Character character)
    {
        //
        _character = character;

        //
        _slotHP     .Init(ECharacterStats.HP     , Manager_Character.Instance.GetStat_Hp(_character));
        _slotATK    .Init(ECharacterStats.ATK    , Manager_Character.Instance.GetStat_Atk(_character));
        _slotDEF    .Init(ECharacterStats.DEF    , Manager_Character.Instance.GetStat_Def(_character));
        _slotAvoid  .Init(ECharacterStats.Avoid  , Manager_Character.Instance.GetStat_Avoid(_character));
        _slotFocus  .Init(ECharacterStats.Focus  , Manager_Character.Instance.GetStat_Focus(_character));
        _slotAtkSpd .Init(ECharacterStats.AtkSpd , Manager_Character.Instance.GetStat_AtkSpd(_character));
        _slotSpeed  .Init(ECharacterStats.Speed  , _character.pTableInfo.speed  );
        _slotCriRate.Init(ECharacterStats.Crirate, Manager_Character.Instance.GetStat_CriRate(_character));
        _slotCriDmg .Init(ECharacterStats.Cridmg , Manager_Character.Instance.GetStat_CriDmg(_character));
    }
}
