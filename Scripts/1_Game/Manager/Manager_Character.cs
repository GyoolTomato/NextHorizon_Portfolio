using System;
using UnityEngine;

public class Manager_Character : Singleton<Manager_Character>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat"></param>
    /// <returns></returns>
    public bool IsPercentStatus(ECharacterStats stat)
    {
        switch (stat)
        {
            case ECharacterStats.Avoid:
            case ECharacterStats.Avoid_Level:
            case ECharacterStats.Focus:
            case ECharacterStats.Focus_level:
            case ECharacterStats.AtkSpd:
            case ECharacterStats.AtkSpd_level:
            case ECharacterStats.Crirate:
            case ECharacterStats.Crirate_level:
            case ECharacterStats.Cridmg:
            case ECharacterStats.Cridmg_level:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_Hp(Character character)
    {
        var temp = character.pTableInfo.hp + (character.pLevel * character.pTableInfo.hp_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_Atk(Character character)
    {
        var temp = character.pTableInfo.atk + (character.pLevel * character.pTableInfo.atk_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_Def(Character character)
    {
        var temp = character.pTableInfo.def + (character.pLevel * character.pTableInfo.def_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_Avoid(Character character)
    {
        var temp = character.pTableInfo.avoid + (character.pLevel * character.pTableInfo.avoid_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_Focus(Character character)
    {
        var temp = character.pTableInfo.focus + (character.pLevel * character.pTableInfo.focus_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_AtkSpd(Character character)
    {
        var temp = character.pTableInfo.atkspd + (character.pLevel * character.pTableInfo.atkspd_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_CriRate(Character character)
    {
        var temp = character.pTableInfo.crirate + (character.pLevel * character.pTableInfo.crirate_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public double GetStat_CriDmg(Character character)
    {
        var temp = character.pTableInfo.cridmg + (character.pLevel * character.pTableInfo.cridmg_level);

        return Math.Round(temp, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void GetExpectLevel(int level, long exp, EItemType[] items, long[] amount, out _107_CharacterLevel.Values resultTableInfo, out long cardExp, out long remainExp)
    {
        //
        resultTableInfo = null;
        cardExp = 0;
        remainExp = 0;

        //
        if (items.Length != amount.Length)
        {
            Debug.LogError("items.Length != amount.Length");
            return;
        }

        //
        for (int i = 0; i < items.Length; i++)
        {
            var expCardValue = 0L;
            switch (items[i])
            {
                case EItemType.ExpCard:
                    var cardTableInfo = _100_CommonValues.GetItem(1000002);
                    if (cardTableInfo == null)
                    {
                        continue;
                    }
                    expCardValue = Convert.ToInt64(cardTableInfo.value) * amount[i];
                    break;
            }

            cardExp += expCardValue;
        }
        var tableInfo = Manager_Table.Instance.GetCharacterLevelInfo(level);
        if (tableInfo == null)
        {
            Debug.LogError("Manager_Table.Instance.GetCharacterLevelInfo(level) == Null");

            return;
        }

        var tempExpRequired = exp + cardExp;
        for (int i = tableInfo.level - 1; i < _107_CharacterLevel.GetList().Count; i++)
        {
            var tempTotalExpRequired = _107_CharacterLevel.GetList()[i];
            if (tempTotalExpRequired.expToNextLevel <= tempExpRequired)
            {
                tempExpRequired -= tempTotalExpRequired.expToNextLevel;
            }
            else
            {
                resultTableInfo = tempTotalExpRequired;
                remainExp = tempExpRequired;
                break;
            }
        }

        if (resultTableInfo == null && _107_CharacterLevel.GetList().Count > 0)
        {
            resultTableInfo = _107_CharacterLevel.GetList()[_107_CharacterLevel.GetList().Count - 1];
            remainExp = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="exp"></param>
    /// <returns></returns>
    public bool DoAddExp(int studentId, int exp)
    {
        //
        var student = GameData.Instance.pDataCharacter.GetCharacter(studentId);
        if (student == null)
            return false;

        //
        student.pExp += exp;

        //
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public bool DoSkillLevelUp_Active(int studentId)
    {
        //
        var student = GameData.Instance.pDataCharacter.GetCharacter(studentId);
        if (student == null)
            return false;

        //
        if (student.pActiveLv >= 5)
            return false;

        student.pActiveLv += 1;

        //
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="skillIndex"></param>
    /// <returns></returns>
    public bool DoSkillLevelUp_Passive(int studentId, int skillIndex)
    {
        //
        var student = GameData.Instance.pDataCharacter.GetCharacter(studentId);
        if (student == null)
            return false;

        //
        if (skillIndex >= student.pPassiveLv.Length)
        {
            return false;
        }
        
        //
        student.pPassiveLv[skillIndex] += 1;

        //
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="charmValue"></param>
    /// <returns></returns>
    public bool DoCharmUp(int studentId, int charmValue)
    {
        //
        var student = GameData.Instance.pDataCharacter.GetCharacter(studentId);
        if (student == null)
            return false;
        
        //
        student.pCharm += charmValue;

        //
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsMaxLevel(int level)
    {
        var maxLevel = _100_CommonValues.GetItem(1000001).value;

        return level >= maxLevel;
    }
}
