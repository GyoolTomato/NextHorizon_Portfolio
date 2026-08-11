using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_Inventory_Armors : Com_Slots<Com_Item_Slot>
{
    /// <summary>
    /// 
    /// </summary>
    public void Init(GameObject textEmpty)
    {
        //
        DeactiveSlots();

        //
        foreach (var item in GameData.Instance.pDataInventory.pDicArmors)
        {
            //
            if (item.Value.pCount == 0)
            {
                continue;
            }

            //
            var slot = ActivateSlot();
            slot.Init(item.Value.pTableInfo);
        }

        //
        textEmpty.SetActive(pSlots.Count == 0);
    }
}
