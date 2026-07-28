using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Character
{
    //
    public bool pIsActive { set; get; } = false;
    public int pGrade { set; get; } = 1;
    public int pStack { set; get; } = 0;
    public int pLevel { set; get; } = 1;
    public int pExp { set; get; } = 0;
    public int pActiveLv { set; get; } = 0;
    public int[] pPassiveLv { set; get; } = new int[3] { 0, 0, 0 };
    public int pCharm { set; get; } = 0;
    public _102_Character.Values pTableInfo { set; get; } = null; 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public Character(_102_Character.Values tableInfo)
    {
        pTableInfo = tableInfo;
    }
}
