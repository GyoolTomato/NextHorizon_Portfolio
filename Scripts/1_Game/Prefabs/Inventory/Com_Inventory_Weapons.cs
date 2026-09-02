using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_Inventory_Weapons : Com_Slots<Com_Weapon_Slot>
{
    /// <summary>
    /// 
    /// </summary>
    public void Init(GameObject textEmpty)
    {
        //
        DeactiveSlots();

        //
        foreach (var item in GameData.Instance.pDataInventory.pDicWeapons)
        {
            //
            var slot = ActivateSlot();
            slot.Init(item.Value, EItemValueType.None);
        }

        //
        textEmpty.SetActive(pSlots.Count == 0);
    }
}
