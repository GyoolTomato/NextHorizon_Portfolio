using UnityEngine;
using UnityEngine.UI;

public class Panel_RewardBox : Panel_Slots<Com_Reward_Slot>
{
    //
    [SerializeField] GridLayoutGroup _grid;


    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        pPanelType = EPanelType.RewardBox;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemKeys"></param>
    /// <param name="armorKeys"></param>
    /// <param name="weaponKeys"></param>
    public void Init(DataItem[] itemKeys, DataArmor[] armorKeys, DataWeapon[] weaponKeys)
    {
        //
        base.Init();

        //
        var dataCount = 0;
        dataCount += itemKeys != null ? itemKeys.Length : 0;
        dataCount += armorKeys != null ? armorKeys.Length : 0;
        dataCount += weaponKeys != null ? weaponKeys.Length : 0;
        _grid.childAlignment = dataCount > _grid.constraintCount ? TextAnchor.UpperLeft : TextAnchor.MiddleCenter;

        //
        DeactiveSlots();

        //
        if (itemKeys != null && itemKeys.Length > 0)
        {
            foreach (var item in itemKeys)
            {
                var slot = ActivateSlot();
                slot.Init(item, EItemValueType.Name);
            }
        }

        //
        if (armorKeys != null && armorKeys.Length > 0)
        {
            foreach(var item in armorKeys)
            {
                var slot = ActivateSlot();
                slot.Init(item, EItemValueType.Name);
            }
        }

        //
        if (weaponKeys != null && weaponKeys.Length > 0)
        {
            foreach(var item in weaponKeys)
            {
                var slot = ActivateSlot();
                slot.Init(item, EItemValueType.Name);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Hide()
    {
        //
        DeactiveSlots();

        //
        base.Hide();
    }
}
