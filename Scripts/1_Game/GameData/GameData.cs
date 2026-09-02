using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    //
    public GameData_PlayerInfo pPlayerInfo   { private set; get; } = null;
    public GameData_Character pDataCharacter { private set; get; } = null;
    public GameData_Gacha     pDataGacha     { private set; get; } = null;
    public GameData_Inventory pDataInventory { private set; get; } = null;
    public GameData_Missions  pDataMissions  { private set; get; } = null;    
    public GameData_Shop      pDataShop      { private set; get; } = null;


    public void Init()
    {
        //
        pPlayerInfo    = new GameData_PlayerInfo();
        pDataCharacter = new GameData_Character();
        pDataGacha     = new GameData_Gacha    ();
        pDataInventory = new GameData_Inventory();
        pDataMissions  = new GameData_Missions ();
        pDataShop      = new GameData_Shop     ();
    }
}
