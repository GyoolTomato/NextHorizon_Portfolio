using UnityEngine;
using System;
using Unity.VisualScripting;

public class SubPanel_CharacterInfo_LevelUp_Btns : Com_Slots<Com_Item_Slot>
{
    /// <summary>
    /// 
    /// </summary>
    public void Init(EItemType[] itemTypes, Action<Com_Item_Slot> onPointerDown, Action<Com_Item_Slot> onPointerPressing, Action<Com_Item_Slot> onPointerUp)
    {
        DeactiveSlots();

        foreach (var item in itemTypes)
        {
            var data = GameData.Instance.pDataInventory.GetDataItem(item);
            if (data == null)
            {
                Debug.LogError("Not found DataItem : " + item);
                continue;
            }

            var temp = new DataItem(data.pCount, data.pTableInfo);

            var slot = ActivateSlot();
            slot.Init(temp, EItemValueType.Name);
            slot.SetPointerControl(onPointerDown, onPointerPressing, onPointerUp);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Com_Item_Slot GetSlot(EItemType itemType, Com_Item_Slot cachedSlot)
    {
        //
        if (cachedSlot == null || cachedSlot.pData.pTableInfo.type != itemType)
        {
            foreach (var item in pSlots)
            {
                if (item.pData.pTableInfo.type == itemType)
                {
                    cachedSlot = item;
                    break;
                }
            }
        }

        //
        return cachedSlot;
    }
}
