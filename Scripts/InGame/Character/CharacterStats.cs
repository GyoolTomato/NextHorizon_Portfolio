using System;
using UnityEngine;

public class CharacterStats
{
    //
    int _baseHpMax;
    int _baseAtk;
    float _baseAtkCoolTime;
    float _baseAtkDelay;
    float _baseAtkDistance;
    int _baseDef;
    int _baseAvoid;
    int _baseFocus;
    float _baseAtkSpd;
    int _baseCriRate;
    float _baseCriDmg;
    float _baseSpeed;

    //
    Character _character;

    //
    public int pHp { private set; get; }
    public int pHpMax
    {
        get
        {
            return _baseHpMax + pHpMax_Increase - pHpMax_Decrease;
        }
    }
    public int pHpMax_Increase { private set; get; }
    public int pHpMax_Decrease { private set; get; }
    public int pAtk
    {
        get
        {
            return _baseAtk + pAtk_Increase - pAtk_Decrease;
        }
    }
    public int pAtk_Increase { private set; get; }
    public int pAtk_Decrease { private set; get; }
    public float pAtkCoolTime => _baseAtkCoolTime;
    public float pAtkCoolDown { set; get; }    
    public float pAtkDelay => _baseAtkDelay;
    public float pAtkDelayCoolDown { set; get; }
    public float pAtkDistance => _baseAtkDistance;
    public int pDef
    {
        get
        {
            return _baseDef + pDef_Increase - pDef_Decrease;
        }
    }
    public int pDef_Increase { private set; get; }
    public int pDef_Decrease { private set; get; }
    public int pAvoid
    {
        get
        {
            return _baseAvoid + pAvoid_Increase - pAvoid_Decrease;
        }
    }
    public int pAvoid_Increase { private set; get; }
    public int pAvoid_Decrease { private set; get; }
    public int pFocus
    {
        get
        {
            return _baseFocus +pFocus_Increase - pFocus_Decrease;
        }
    }
    public int pFocus_Increase { private set; get; }
    public int pFocus_Decrease { private set; get; }
    public float pAtkSpd
    {
        get
        {
            return _baseAtkSpd + pAtkSpd_Increase - pAtkSpd_Decrease;
        }
    }
    public float pAtkSpd_Increase { private set; get; }
    public float pAtkSpd_Decrease { private set; get; }
    public int pCriRate
    {
        get
        {
            return _baseCriRate + pCriRate_Increase - pCriRate_Decrease;
        }
    }
    public int pCriRate_Increase { private set; get; }
    public int pCriRate_Decrease { private set; get; }
    public float pCriDmg
    {
        get
        {
            return _baseCriDmg + pCriDmg_Increase - pCriDmg_Decrease;
        }
    }
    public float pCriDmg_Increase { private set; get; }
    public float pCriDmg_Decrease { private set; get; }
    public float pSpeed
    {
        get
        {
            return _baseSpeed + pSpeed_Increase - pSpeed_Decrease;
        }
    }
    public float pSpeed_Increase { private set; get; }
    public float pSpeed_Decrease { private set; get; }

    int pDamage { set; get; }

    //
    public bool pIsCorpse { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public CharacterStats(Character character)
    {
        //
        _character = character;
        var data = _character.pData.pData;

        //
        _baseHpMax       = Convert.ToInt32 (Manager_Character.Instance.GetTotalStat(data, ECharacterStats.HP     ));
        _baseAtk         = Convert.ToInt32 (Manager_Character.Instance.GetTotalStat(data, ECharacterStats.ATK    ));
        _baseAtkCoolTime = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.AtkCoolTime));
        _baseAtkDelay    = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.AtkDelay));
        _baseAtkDistance = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.AtkDistance));
        _baseDef         = Convert.ToInt32 (Manager_Character.Instance.GetTotalStat(data, ECharacterStats.DEF    ));
        _baseAvoid       = Convert.ToInt32(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.Avoid  ));
        _baseFocus       = Convert.ToInt32(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.Focus  ));
        _baseAtkSpd      = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.AtkSpd ));
        _baseCriRate     = Convert.ToInt32(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.Crirate));
        _baseCriDmg      = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.Cridmg ));
        _baseSpeed       = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(data, ECharacterStats.Speed  ));

        //
        Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        pHpMax_Increase = 0;
        pHpMax_Decrease = 0;

        pAtk_Increase = 0;
        pAtk_Decrease = 0;

        pDef_Increase = 0;
        pDef_Decrease = 0;

        pAvoid_Increase = 0;
        pAvoid_Decrease = 0;

        pFocus_Increase = 0;
        pFocus_Decrease = 0;

        pAtkSpd_Increase = 0f;
        pAtkSpd_Decrease = 0f;

        pCriRate_Increase = 0;
        pCriRate_Decrease = 0;

        pCriDmg_Increase = 0f;
        pCriDmg_Decrease = 0f;

        pSpeed_Increase = 0f;
        pSpeed_Decrease = 0f;

        pDamage = 0;

        //
        pHp = pHpMax;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat">HP, ATK, DEF</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool InCreaseStat(ECharacterStats stat, int value)
    {
        if (value < 0)
        {
            Debug.LogError("value error");
            return false;
        }
        else if (value == 0)
        {
            return false;
        }
        else
        {
            switch (stat)
            {
                case ECharacterStats.HP : pHpMax_Increase  += value; break;
                case ECharacterStats.ATK: pAtkSpd_Increase += value; break;
                case ECharacterStats.DEF: pDef_Increase += value; break;
                default:
                    Debug.LogError($"Unsupported stat : {stat}");
                    return false;
            }


            return true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat">HP, ATK, DEF</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool DecreaseStat(ECharacterStats stat, int value)
    {
        if (value < 0)
        {
            Debug.LogError("value error");
            return false;
        }
        else if (value == 0)
        {
            return false;
        }
        else
        {
            switch (stat)
            {
                case ECharacterStats.HP: pHpMax_Decrease += value; break;
                case ECharacterStats.ATK: pAtkSpd_Decrease += value; break;
                case ECharacterStats.DEF: pDef_Decrease += value; break;
                default:
                    Debug.LogError($"Unsupported stat : {stat}");
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat">Avoid, Focus, AtkSpd, Speed, CriRate, CriDmg</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool InCreaseStat(ECharacterStats stat, float value)
    {
        if (value < 0)
        {
            Debug.LogError("value is 0 under");
            return false;
        }
        else if (value == 0)
        {
            return false;
        }
        else
        {
            switch (stat)
            {
                case ECharacterStats.AtkSpd : pAtkSpd_Increase  += value; break;
                case ECharacterStats.Speed  : pSpeed_Increase   += value; break;
                case ECharacterStats.Cridmg : pCriDmg_Increase  += value; break;
                default:
                    Debug.LogError($"Unsupported stat : {stat}");
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stat">Avoid, Focus, AtkSpd, Speed, CriRate, CriDmg</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool DecreaseStat(ECharacterStats stat, float value)
    {
        if (value < 0)
        {
            Debug.LogError("value is 0 under");
            return false;
        }
        else if (value == 0)
        {
            return false;
        }
        else
        {
            switch (stat)
            {
                case ECharacterStats.AtkSpd: pAtkSpd_Decrease += value; break;
                case ECharacterStats.Speed: pSpeed_Decrease += value; break;
                case ECharacterStats.Cridmg: pCriDmg_Decrease += value; break;
                default:
                    Debug.LogError($"Unsupported stat : {stat}");
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void AddDamage(int damage)
    {
        pDamage = Mathf.Max(0, pDamage + damage);

        pHp -= pDamage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return pHp <= 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetCorpse(bool isCorpse)
    {
        pIsCorpse = isCorpse;
    }
}
