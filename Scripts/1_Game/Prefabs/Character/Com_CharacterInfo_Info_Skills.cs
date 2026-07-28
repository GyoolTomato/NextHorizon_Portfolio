using UnityEngine;

public class Com_CharacterInfo_Info_Skills : Com_Base
{
    //
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotActive    = null;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotPassive_0 = null;
    [SerializeField] Com_CharacterInfo_Info_Skills_Slot _slotPassive_1 = null;

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
        _slotActive   .Init(_character.pTableInfo.activeSkill);
        _slotPassive_0.Init(_character.pTableInfo.passiveSkill_0);
        _slotPassive_1.Init(_character.pTableInfo.passiveSkill_1);
    }
}
