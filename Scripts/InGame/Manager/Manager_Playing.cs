using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Manager_Playing : Singleton<Manager_Playing>
{
    //
    Player _player;
    Map _map;

    //
    public int pBattleResult { private set; get; }
    public int pBattleResult_RemainTime { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        var camObject = GameObject.Find("Player");
        _player = camObject.GetComponent<Player>();

        var map = GameObject.Find("Map");
        _map = map.GetComponent<Map>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool SetPlayerCam()
    {
        if (_player == null)
        {
            Debug.LogError("player is Null");
            return false;
        }

        _player.transform.SetParent(GameData.Instance.pDataInGame.pPlayer.transform);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Transform GetCameraTransform()
    {
        return _player.GetCameraTransform();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public Vector3 GetSpawnPoint(ESpawnPoint spawnPoint)
    {
        return _map.GetSpawnPoint(spawnPoint);
    }

    /// <summary>
    /// 0 : Lose
    /// 1 : Win
    /// 2 : Fast
    /// 3 : Very Fast
    /// </summary>
    public void SetBattleResult(bool isAllKill, bool isFast, bool isVeryFast, int remainTime)
    {
        //
        var result = 0;

        if (isAllKill)
        {
            //
            result += 1;

            //
            if (isFast)
            {
                result += 1;
            }

            //
            if (isVeryFast)
            {
                result += 1;
            }
        }

        //
        pBattleResult = result;
        pBattleResult_RemainTime = remainTime;
    }
}
