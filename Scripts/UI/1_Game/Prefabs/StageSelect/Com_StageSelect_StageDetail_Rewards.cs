using UnityEngine;

public class Com_StageSelect_StageDetail_Rewards : Com_Slots<Com_Reward_Slot>
{
    //
    DataStage _data;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void Init(DataStage data)
    {
        //
        base.Init();

        //
        DeactiveSlots();

        var crystalSlot = ActivateSlot();
        crystalSlot.Init(_101_Items.GetItem(1010001), EItemValueType.None, data.pTableInfo.crystalCount);

        var goldSlot = ActivateSlot();
        goldSlot.Init(_101_Items.GetItem(1010002), EItemValueType.None, data.pTableInfo.goldCount);

        var armorGroup = _109_ArmorGroup.GetItem(data.pTableInfo.armorGroup);
        if (armorGroup != null)
        {
            foreach (var item in armorGroup.rewardKey)
            {
                if (item == 0)
                {
                    continue;
                }

                var temp = _105_Armors.GetItem(item);
                if (_105_Armors.GetItem(item) != null)
                {
                    var tempSlot = ActivateSlot();
                    tempSlot.Init(temp, EItemValueType.None);
                }
            }
        }

        var weaponGroup = _110_WeaponGroup.GetItem(data.pTableInfo.weaponGroup);
        if (weaponGroup != null)
        {
            foreach (var item in weaponGroup.rewardKey)
            {
                if (item == 0)
                {
                    continue;
                }

                var temp = _106_Weapons.GetItem(item);
                if (_106_Weapons.GetItem(item) != null)
                {
                    var tempSlot = ActivateSlot();
                    tempSlot.Init(temp, EItemValueType.None);
                }
            }
        }
    }
}
