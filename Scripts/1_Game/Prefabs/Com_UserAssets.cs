using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_UserAssets : Com_Slots<Com_UserAsset>
{
    /// <summary>
    /// 
    /// </summary>
    public void Init(params EItemType[] itemTypes)
    {
        //
        DeactiveSlots();

        //
        foreach (var itemType in itemTypes)
        {
            //var slot = ActivateSlot();
            //slot.Init(itemType);
        }
    }
}
