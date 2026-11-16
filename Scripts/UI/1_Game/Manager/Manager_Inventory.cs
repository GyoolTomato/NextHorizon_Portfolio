using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

public class Manager_Inventory : Singleton<Manager_Inventory>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataItem data, EItemValueType itemValueType)
    {
        //
        if (data == null)
        {
            return "---";
        }

        //
        var temp = string.Empty;

        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextItem(data.pTableInfo.name);                
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
        }

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataArmor data, EItemValueType itemValueType)
    {        
        //
        if (data == null)
        {
            return "---";
        }

        //
        var temp = string.Empty;


        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextArmor(data.pTableInfo.name);
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
            case EItemValueType.Level:
                temp = string.Format(Manager_UI.Instance.GetTextCommon(9000039), data.pLevel);
                break;
        }

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataWeapon data, EItemValueType itemValueType)
    {
        //
        if (data == null)
        {
            return "---";
        }

        //
        var temp = string.Empty;

        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextWeapon(data.pTableInfo.name);
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
            case EItemValueType.Level:
                temp = string.Format(Manager_UI.Instance.GetTextCommon(9000039), data.pLevel);
                break;
        }

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterKey"></param>
    /// <param name="characterStat"></param>
    /// <returns></returns>
    public decimal GetCharacterEquipStat(int characterKey, ECharacterStats characterStat)
    {
        var result = 0m;

        var dataInventory = GameData.Instance.pDataInventory;

        var dataWeapon = dataInventory.GetCharacterWeapon(characterKey);
        result += dataWeapon != null ? GetEquipStat(dataWeapon, characterStat) : 0m;

        var dataArmor_Hat = dataInventory.GetCharacterArmor(characterKey, EEquipmentType.Hat);
        result += dataArmor_Hat != null ? GetEquipStat(dataArmor_Hat, characterStat) : 0m;

        var dataArmor_Clothes = dataInventory.GetCharacterArmor(characterKey, EEquipmentType.Clothes);
        result += dataArmor_Clothes != null ? GetEquipStat(dataArmor_Clothes, characterStat) : 0m;

        var dataArmor_Gloves = dataInventory.GetCharacterArmor(characterKey, EEquipmentType.Gloves);
        result += dataArmor_Gloves != null ? GetEquipStat(dataArmor_Gloves, characterStat) : 0m;

        var dataArmor_Shoes = dataInventory.GetCharacterArmor(characterKey, EEquipmentType.Shoes);
        result += dataArmor_Shoes != null ? GetEquipStat(dataArmor_Shoes, characterStat) : 0m;

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    void GetExpectLevel(EItemType[] items, long[] amount, out long cardExp)
    {
        //
        cardExp = 0;

        //
        if (items.Length != amount.Length)
        {
            UnityEngine.Debug.LogError("items.Length != amount.Length");
            return;
        }

        //
        for (int i = 0; i < items.Length; i++)
        {
            var expCardValue = 0L;
            switch (items[i])
            {
                case EItemType.ReinforcementCard:
                    var cardTableInfo = _100_CommonValues.GetItem(1000005);
                    if (cardTableInfo == null)
                    {
                        continue;
                    }
                    expCardValue = Convert.ToInt64(cardTableInfo.value_int) * amount[i];
                    break;
            }

            cardExp += expCardValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="exp"></param>
    /// <param name="items"></param>
    /// <param name="amount"></param>
    /// <param name="resultTableInfo"></param>
    /// <param name="cardExp"></param>
    /// <param name="remainExp"></param>
    public void GetExpectLevel_Weapon(int level, long exp, EItemType[] items, long[] amount, out _111_WeaponLevel.Values resultTableInfo, out long cardExp, out long remainExp)
    {
        //
        resultTableInfo = null;
        remainExp = 0;

        //
        GetExpectLevel(items, amount, out cardExp);        

        //
        var tableInfo = Manager_Table.Instance.GetWeaponLevelInfo(level);
        if (tableInfo == null)
        {
            UnityEngine.Debug.LogError("Manager_Table.Instance.GetWeaponLevelInfo(level) == Null");

            return;
        }

        var tempExpRequired = exp + cardExp;
        for (int i = tableInfo.level - 1; i < _111_WeaponLevel.GetList().Count; i++)
        {
            var tempTotalExpRequired = _111_WeaponLevel.GetList()[i];
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

        if (resultTableInfo == null && _111_WeaponLevel.GetList().Count > 0)
        {
            resultTableInfo = _111_WeaponLevel.GetList()[_111_WeaponLevel.GetList().Count - 1];
            remainExp = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="exp"></param>
    /// <param name="items"></param>
    /// <param name="amount"></param>
    /// <param name="resultTableInfo"></param>
    /// <param name="cardExp"></param>
    /// <param name="remainExp"></param>
    public void GetExpectLevel_Armor(int level, long exp, EItemType[] items, long[] amount, out _112_ArmorLevel.Values resultTableInfo, out long cardExp, out long remainExp)
    {
        //
        resultTableInfo = null;
        remainExp = 0;

        //
        GetExpectLevel(items, amount, out cardExp);

        //
        var tableInfo = Manager_Table.Instance.GetArmorLevelInfo(level);
        if (tableInfo == null)
        {
            UnityEngine.Debug.LogError("Manager_Table.Instance.GetArmorLevelInfo(level) == Null");

            return;
        }

        var tempExpRequired = exp + cardExp;
        for (int i = tableInfo.level - 1; i < _112_ArmorLevel.GetList().Count; i++)
        {
            var tempTotalExpRequired = _112_ArmorLevel.GetList()[i];
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

        if (resultTableInfo == null && _112_ArmorLevel.GetList().Count > 0)
        {
            resultTableInfo = _112_ArmorLevel.GetList()[_112_ArmorLevel.GetList().Count - 1];
            remainExp = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterStat"></param>
    /// <returns></returns>
    public decimal GetEquipStat(DataArmor data, ECharacterStats characterStat)
    {
        //
        var result = 0m;

        //
        if (data == null)
        {
            return result;
        }

        //
        if (data.pTableInfo.mainValueType == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.mainValue) + (Convert.ToDecimal(data.pTableInfo.mainValue_level) * Convert.ToDecimal(data.pLevel));
        }

        if (data.pSubValueType_0 == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
        }

        if (data.pSubValueType_1 == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
        }

        //
        return result;
    }

    public decimal GetEquipStat(DataWeapon data, ECharacterStats characterStat)
    {
        //
        var result = 0m;

        //
        if (data == null)
        {
            return result;
        }

        //
        if (data.pTableInfo.mainValueType == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.mainValue) + (Convert.ToDecimal(data.pTableInfo.mainValue_level) * Convert.ToDecimal(data.pLevel));
        }

        if (data.pSubValueType_0 == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
        }

        if (data.pSubValueType_1 == characterStat)
        {
            result += Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
        }

        //
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public decimal GetEquipStat(DataWeapon data, EEquipStatType equipStatType)
    {
        var result = 0m;

        switch (equipStatType)
        {
            case EEquipStatType.Main:
                result = Convert.ToDecimal(data.pTableInfo.mainValue) + (Convert.ToDecimal(data.pTableInfo.mainValue_level) * Convert.ToDecimal(data.pLevel));
                break;
            case EEquipStatType.Sub_0:
            case EEquipStatType.Sub_1:
                result = Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
                break;
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public decimal GetEquipStat(DataArmor data, EEquipStatType equipStatType)
    {
        var result = 0m;

        switch (equipStatType)
        {
            case EEquipStatType.Main:
                result = Convert.ToDecimal(data.pTableInfo.mainValue) + (Convert.ToDecimal(data.pTableInfo.mainValue_level) * Convert.ToDecimal(data.pLevel));
                break;
            case EEquipStatType.Sub_0:
            case EEquipStatType.Sub_1:
                result = Convert.ToDecimal(data.pTableInfo.subValue) + (Convert.ToDecimal(data.pTableInfo.subValue_level) * Convert.ToDecimal(data.pLevel));
                break;
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsMaxLevel_Weapon(int level)
    {
        var maxLevel = _100_CommonValues.GetItem(1000003).value_int;

        return level >= maxLevel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsMaxLevel_Armor(int level)
    {
        var maxLevel = _100_CommonValues.GetItem(1000004).value_int;

        return level >= maxLevel;
    }
}
