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
    public _105_Armors.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public DataArmor(int id, int level, int exp, int equipCharacter, _105_Armors.Values tableInfo)
    {
        pId = id;
        pLevel = level;
        pExp = exp;
        pEquipedCharacter = equipCharacter;
        pTableInfo = tableInfo;
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
    public _106_Weapons.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public DataWeapon(int id, int level, int exp, int equipCharacter, _106_Weapons.Values tableInfo)
    {
        pId = id;
        pLevel = level;
        pExp = exp;
        pEquipedCharacter = equipCharacter;
        pTableInfo = tableInfo;
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
public class GameData_Inventory
{
    

    //
    public Dictionary<EItemType, DataItem> pDicItems { private set; get; }

    public Dictionary<int, DataArmor> pDicArmors { private set; get; }

    public Dictionary<int, DataWeapon> pDicWeapons { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerPlayerItemData[] items, ServerPlayerArmorData[] armors, ServerPlayerWeaponData[] weapons)
    {
        //
        pDicItems ??= new Dictionary<EItemType, DataItem>();

        pDicItems.Clear();

        if (items != null)
        {
            foreach (var item in items)
            {
                var tableInfo = _101_Items.GetItem(item.itemKey);
                if (tableInfo == null)
                    continue;

                var temp = new DataItem(item.quantity, tableInfo);

                if (pDicItems.ContainsKey(temp.pTableInfo.type) == false)
                    pDicItems.Add(temp.pTableInfo.type, temp);
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

                var temp = new DataArmor(item.id, item.level, item.exp, item.equipedCharacter ,tableInfo);

                if (pDicArmors.ContainsKey(temp.pTableInfo.key) == false)
                    pDicArmors.Add(temp.pTableInfo.key, temp);
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

                var temp = new DataWeapon(item.id, item.level, item.exp, item.equipedCharacter, tableInfo);

                if (pDicWeapons.ContainsKey(temp.pTableInfo.key) == false)
                    pDicWeapons.Add(temp.pTableInfo.key, temp);
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
        if (pDicItems.ContainsKey(itemType) == false)
        {
            //
            var tableInfo = Manager_Table.Instance.GetItemInfo(itemType);
            if (tableInfo == null)
                return null;

            //
            pDicItems.Add(itemType, new DataItem(0, tableInfo));
        }

        //
        return pDicItems[itemType];
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
        var dicItem = GameData.Instance.pDataInventory.pDicItems;
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
}
