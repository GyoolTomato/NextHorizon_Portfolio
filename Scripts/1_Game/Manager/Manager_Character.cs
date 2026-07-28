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
}
