using System;
using System.Collections.Generic;
using System.Text;


public class Manager_Calculate : Singleton<Manager_Calculate>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsPrize(int value)
    {
        var result = UnityEngine.Random.Range(0, 10000);
        return result <= value;
    }
}
