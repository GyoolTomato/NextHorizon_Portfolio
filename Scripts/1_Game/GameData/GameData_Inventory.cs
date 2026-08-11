using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Inventory
{
    /// <summary>
    /// 
    /// </summary>
    public class DataItem : Data_Item_Base<_101_Items.Values>
    {
        public DataItem(_101_Items.Values tableInfo, long count) 
            : base (tableInfo, count)
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataArmors : Data_Item_Base<_105_Armors.Values>
    {
        public DataArmors(_105_Armors.Values tableInfo, long count)
            : base(tableInfo, count)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataWeapon : Data_Item_Base<_106_Weapons.Values>
    {
        public DataWeapon(_106_Weapons.Values tableInfo, long count)
            : base(tableInfo, count)
        {

        }
    }

    //
    public Dictionary<EItemType, DataItem> pDicItems { private set; get; }

    public Dictionary<int, DataArmors> pDicArmors { private set; get; }

    public Dictionary<int, DataWeapon> pDicWeapons { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        pDicItems ??= new Dictionary<EItemType, DataItem>();

        pDicItems.Clear();

        //
        pDicArmors ??= new Dictionary<int, DataArmors>();

        pDicArmors.Clear();

        //
        pDicWeapons ??= new Dictionary<int, DataWeapon>();

        pDicWeapons.Clear();
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
            pDicItems.Add(itemType, new DataItem(tableInfo, 0));
        }

        //
        return pDicItems[itemType];
    }
}
