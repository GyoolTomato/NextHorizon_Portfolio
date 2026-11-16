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
    DataCharacter _character = null;   


    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    public void Init(DataCharacter character)
    {
        //
        _character = character;

        //
        _slotHP     .Init(ECharacterStats.HP     , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.HP));
        _slotATK    .Init(ECharacterStats.ATK    , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.ATK));
        _slotDEF    .Init(ECharacterStats.DEF    , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.DEF));
        _slotAvoid  .Init(ECharacterStats.Avoid  , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Avoid));
        _slotFocus  .Init(ECharacterStats.Focus  , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Focus));
        _slotAtkSpd .Init(ECharacterStats.AtkSpd , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.AtkSpd));
        _slotSpeed  .Init(ECharacterStats.Speed  , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Speed));
        _slotCriRate.Init(ECharacterStats.Crirate, Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Crirate));
        _slotCriDmg .Init(ECharacterStats.Cridmg , Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Cridmg));
    }
}
