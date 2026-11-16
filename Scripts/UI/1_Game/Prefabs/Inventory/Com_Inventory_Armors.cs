using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_Inventory_Armors : Com_Slots<Com_Armor_Slot>
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
            var slot = ActivateSlot();
            slot.Init(item.Value, EItemValueType.Level);
        }

        //
        textEmpty.SetActive(pSlots.Count == 0);
    }
}
