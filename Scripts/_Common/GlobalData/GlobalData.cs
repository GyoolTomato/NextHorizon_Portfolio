using System;
using System.Collections.Generic;
using System.Text;

public class GlobalData : Singleton<GlobalData>
{
    //
    public GlobalData_Main pDataMain { private set; get; } = null;
    public GlobalData_PlayerInfo pDataPlayerInfo { private set; get; } = null;


    public void Init()
    {
        //
        pDataMain = new GlobalData_Main();
        pDataPlayerInfo = new GlobalData_PlayerInfo();
    }
}
