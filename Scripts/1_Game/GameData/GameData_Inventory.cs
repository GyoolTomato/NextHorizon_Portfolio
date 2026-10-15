using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class DataItem
{
    //
    public long pCount { private set; get; }
    public _101_Items.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="count"></param>
    public DataItem(long count, _101_Items.Values tableInfo)
    {
        pCount = count;
        pTableInfo = tableInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetItemCount(long count)
    {
        //
        if (count < 0)
            count = 0;

        //
        this.pCount = count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void SubtractItemCount(long count)
    {
        SetItemCount(pCount - count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    public void AddItemCount(long count)
    {
        SetItemCount(pCount + count);
    }
}

/// <summary>
/// 
/// </summary>
public class DataArmor
{
    //
    public int pId { private set; get; }
    public int pLevel { private set; get; }
    public int pExp { private set; get; }
    public int pEquipedCharacter { private set; get; }
    public ECharacterStats pSubValueType_0 { private set; get; }
    public ECharacterStats pSubValueType_1 { private set; get; }
    public _105_Armors.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public DataArmor(int id, int level, int exp, int equipCharacter, string subType0, string subType1, _105_Armors.Values tableInfo)
    {
        //
        pId = id;
        pLevel = level;
        pExp = exp;
        pEquipedCharacter = equipCharacter;
        pTableInfo = tableInfo;

        //
        if (Enum.TryParse<ECharacterStats>(subType0, out var type0))
            pSubValueType_0 = type0;
        else
            pSubValueType_0 = ECharacterStats.None;

        if (Enum.TryParse<ECharacterStats>(subType1, out var type1))
            pSubValueType_1 = type1;
        else
            pSubValueType_1 = ECharacterStats.None;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetLevel(int level)
    {
        pLevel = level;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetExp(int exp)
    {
        pExp = exp;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetEquipCharacter(int characterKey)
    {
        pEquipedCharacter = characterKey;
    }
}

/// <summary>
/// 
/// </summary>
public class DataWeapon
{
    //
    public int pId { private set; get; }
    public int pLevel { private set; get; }
    public int pExp { private set; get; }
    public int pEquipedCharacter { private set; get; }
    public ECharacterStats pSubValueType_0 { private set; get; }
    public ECharacterStats pSubValueType_1 { private set; get; }
    public _106_Weapons.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public DataWeapon(int id, int level, int exp, int equipCharacter, string subType0, string subType1, _106_Weapons.Values tableInfo)
    {
        //
        pId = id;
        pLevel = level;
        pExp = exp;
        pEquipedCharacter = equipCharacter;
        pTableInfo = tableInfo;

        //
        if (Enum.TryParse<ECharacterStats>(subType0, out var type0))
            pSubValueType_0 = type0;
        else
            pSubValueType_0 = ECharacterStats.None;

        if (Enum.TryParse<ECharacterStats>(subType1, out var type1))
            pSubValueType_1 = type1;
        else
            pSubValueType_1 = ECharacterStats.None;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetLevel(int level)
    {
        pLevel = level;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetExp(int exp)
    {
        pExp = exp;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetEquipCharacter(int characterKey)
    {
        pEquipedCharacter = characterKey;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public decimal GetMainValue()
    {
        var temp = pTableInfo.mainValue + (pTableInfo.mainValue_level * pLevel);
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public decimal GetSubValue()
    {
        var temp = pTableInfo.subValue + (pTableInfo.subValue * pLevel);
        return temp;
    }
}

/// <summary>
/// 
/// </summary>
public class GameData_Inventory
{
    //
    public Dictionary<int, DataItem> pDicItems { private set; get; } = new Dictionary<int, DataItem>();
    public Dictionary<EItemType, DataItem> pDicItems_Type { private set; get; } = new Dictionary<EItemType, DataItem>();

    public Dictionary<int, DataArmor> pDicArmors { private set; get; } = new Dictionary<int, DataArmor>();

    public Dictionary<int, DataWeapon> pDicWeapons { private set; get; } = new Dictionary<int, DataWeapon>();


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerPlayerItemData[] items, ServerPlayerArmorData[] armors, ServerPlayerWeaponData[] weapons)
    {
        //
        pDicItems ??= new Dictionary<int, DataItem>();
        pDicItems_Type ??= new Dictionary<EItemType, DataItem>();

        pDicItems.Clear();

        if (items != null)
        {
            foreach (var item in items)
            {
                var tableInfo = _101_Items.GetItem(item.itemKey);
                if (tableInfo == null)
                    continue;

                var temp = new DataItem(item.quantity, tableInfo);

                //
                if (pDicItems.ContainsKey(temp.pTableInfo.key) == false)
                    pDicItems.Add(temp.pTableInfo.key, temp);

                //
                if (pDicItems_Type.ContainsKey(temp.pTableInfo.type) == false)
                    pDicItems_Type.Add(temp.pTableInfo.type, temp);
            }
        }

        //
        pDicArmors ??= new Dictionary<int, DataArmor>();

        pDicArmors.Clear();

        if (items != null)
        {
            foreach (var item in armors)
            {
                var tableInfo = _105_Armors.GetItem(item.armorKey);
                if (tableInfo == null)
                    continue;

                var temp = new DataArmor(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);

                if (pDicArmors.ContainsKey(temp.pId) == false)
                    pDicArmors.Add(temp.pId, temp);
            }
        }

        //
        pDicWeapons ??= new Dictionary<int, DataWeapon>();

        pDicWeapons.Clear();

        if (weapons != null)
        {
            foreach (var item in weapons)
            {
                var tableInfo = _106_Weapons.GetItem(item.weaponKey);
                if (tableInfo == null)
                    continue;

                var temp = new DataWeapon(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);

                if (pDicWeapons.ContainsKey(temp.pId) == false)
                    pDicWeapons.Add(temp.pId, temp);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataItem GetDataItem(EItemType itemType)
    {
        //
        if (pDicItems_Type.ContainsKey(itemType) == false)
        {
            //
            var tableInfo = Manager_Table.Instance.GetItemInfo(itemType);
            if (tableInfo == null)
                return null;

            //
            pDicItems_Type.Add(itemType, new DataItem(0, tableInfo));
        }

        //
        return pDicItems_Type[itemType];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataArmor GetDataArmor(int id)
    {
        if (pDicArmors.ContainsKey(id) == false)
        {
            return null;
        }

        return pDicArmors[id];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataWeapon GetDataWeapon(int id)
    {
        if (pDicWeapons.ContainsKey(id) == false)
        {
            return null;
        }

        return pDicWeapons[id];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool IsAbleToUseItem(EItemType itemType, long count)
    {
        //
        if (count <= 0)
            return false;

        //
        var dicItem = GameData.Instance.pDataInventory.pDicItems_Type;
        if (dicItem.ContainsKey(itemType))
        {
            if (dicItem[itemType].pCount >= count)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataArmor GetCharacterArmor(int characterKey, EEquipmentType type)
    {
        //
        foreach (var item in pDicArmors)
        {
            if (item.Value.pEquipedCharacter == characterKey && item.Value.pTableInfo.type == type)
            {
                return item.Value;
            }
        }

        //
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataWeapon GetCharacterWeapon(int characterKey)
    {
        //
        foreach (var item in pDicWeapons)
        {
            if (item.Value.pEquipedCharacter == characterKey)
                return item.Value;
        }

        //
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterKey"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public void SetCharacterArmor(int characterKey, int id)
    {
        var data = GetDataArmor(id);
        if (data == null)
            return;

        foreach (var armor in pDicArmors.Values)
        {
            if (armor.pId == id)
                continue;

            if (armor.pEquipedCharacter == characterKey &&
                armor.pTableInfo.type == data.pTableInfo.type)
            {
                armor.SetEquipCharacter(0);
            }
        }

        data.SetEquipCharacter(characterKey);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterKey"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public void SetCharacterWeapon(int characterKey, int id)
    {
        var data = GetDataWeapon(id);
        if (data == null)
            return;

        foreach (var weapon in pDicWeapons.Values)
        {
            if (weapon.pId == id)
                continue;

            if (weapon.pEquipedCharacter == characterKey)
                weapon.SetEquipCharacter(0);
        }

        data.SetEquipCharacter(characterKey);
    }

    public void SetArmorEquipCharacter(int id, int characterKey)
    {
        var data = GetDataArmor(id);
        if (data == null)
            return;

        data.SetEquipCharacter(characterKey);
    }

    public void SetWeaponEquipCharacter(int id, int characterKey)
    {
        var data = GetDataWeapon(id);
        if (data == null)
            return;

        data.SetEquipCharacter(characterKey);
    }
}
