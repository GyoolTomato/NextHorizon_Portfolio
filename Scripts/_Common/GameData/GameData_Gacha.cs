using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameData_Gacha
{
    //
    public int[] pGachaCount { set; get; } = new int[2] { 0, 0 };


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    public bool AddGachaCount(int index, int count)
    {
        //
        if (index < 0 || index >= pGachaCount.Length)
        {
            return false;
        }

        //
        pGachaCount[index] += count;
        if (pGachaCount[index] < 0) pGachaCount[index] = 0;

        return true;
    }

    
}
