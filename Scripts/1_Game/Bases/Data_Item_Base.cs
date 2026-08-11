using System;
using System.Collections.Generic;
using System.Text;

public class Data_Item_Base<T>
{
    //
    public T pTableInfo { private set; get; }
    public long pCount { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTableInfo"></param>
    /// <param name="pCount"></param>
    public Data_Item_Base(T tableInfo, long count)
    {
        pTableInfo = tableInfo;
        pCount = count;
    }

    /// <summary>
    /// 
    /// </summary>
    void SetItemCount(long count)
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
    public void UseItemCount(long count)
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
