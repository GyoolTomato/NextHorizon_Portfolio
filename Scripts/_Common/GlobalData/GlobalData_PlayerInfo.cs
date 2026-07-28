using System;
using System.Collections.Generic;
using System.Text;

public class GlobalData_PlayerInfo
{
    //
    public string pUserId { private set; get; }
    public string pUserNickName { private set; get; }
    public long pLevel { private set; get; }
    public long pExp { private set; get; }
    public long pGold { private set; get; }
    public long pDiamond { private set; get; }
    Dictionary<EItemType, long> pDicItem { set; get; } = new Dictionary<EItemType, long>();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userData"></param>
    public void Init(UserData userData)
    {
        pUserId = userData.uid;
        pUserNickName = userData.nickname;
        pLevel = userData.level;
        pExp = userData.exp;
        pGold = userData.gold;
        pDiamond = 0;

        pDicItem ??= new Dictionary<EItemType, long>();
        pDicItem.Clear();

        pDicItem.Add(EItemType.ExpCard, 100);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public long GetItemCount(EItemType itemType)
    {
        //
        if (pDicItem.ContainsKey(itemType))
            return pDicItem[itemType];

        //
        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool AddItemCount(EItemType itemType, long count)
    {
        //
        if (count <= 0)
            return false;

        //
        if (pDicItem.ContainsKey(itemType))
            pDicItem[itemType] += count;
        else
            pDicItem.Add(itemType, count);

        //
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool RemoveItemCount(EItemType itemType, long count)
    {
        //
        if (count <= 0)
            return false;

        //
        if (IsAbleToUseItem(itemType, count))
        {
            pDicItem[itemType] -= count;
            if (pDicItem[itemType] < 0)
                pDicItem[itemType] = 0;

            return true;
        }

        //
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool SetItemCount(EItemType itemType, long count)
    {
        //
        if (count < 0)
            return false;

        //
        if (pDicItem.ContainsKey(itemType))
            pDicItem[itemType] = count;
        else
            pDicItem.Add(itemType, count);

        //
        return true;
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
        if (pDicItem.ContainsKey(itemType))
        {
            if (pDicItem[itemType] >= count)
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
