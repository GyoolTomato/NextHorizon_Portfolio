using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayState_Playing : PlayState
{
    //
    public Dictionary<float, List<DataInGameCharacter>> _dicStageEnemies_Timer { private set; get; }

    //
    int _monsterCount;

    //
    float _spawnCoolTime;
    float[] _spawnCoolDown;


    //
    public PlayState_Playing(EPlayState state) : base(state)
    {
        
    }

    //
    override public void Enter()
    {
        //
        var monsterTable = _701_StageMonster.GetItem(GameData.Instance.pDataInGame.pStageInfo.pTableInfo.monsterTable);
        if (monsterTable == null)
        {
            Debug.LogError("monsterTable is Null.");
        }
        for (int i = 0; i < monsterTable.monster.Length; i++)
        {
            var monsterTableInfo = _102_Character.GetItem(monsterTable.monster[i]);
            if (monsterTableInfo != null)
            {
                _monsterCount += monsterTable.count[i];
            }
        }

        //
        _spawnCoolTime = Convert.ToSingle(_100_CommonValues.GetItem(1000011).value_double);
        _spawnCoolDown = new float[8];

        //
        Manager_Game.StartGameTimer();
    }

    //
    override public void Exit()
    {

    }

    //
    override public void Update()
    {
        //
        GameData.Instance.pDataInGame.pRemainTime -= Manager_Game.pGameDeltaTime;

        //
        if (GameData.Instance.pDataInGame.pRemainTime <= 0 ||
            GameData.Instance.pDataInGame.pPlayer.pComStats.IsDead() ||
            IsAllKill())
        {
            //
            var isFast = GameData.Instance.pDataInGame.pRemainTime > _100_CommonValues.GetItem(1000013).value_int;
            var isVeryFast = GameData.Instance.pDataInGame.pRemainTime > _100_CommonValues.GetItem(1000014).value_int;

            Manager_Playing.Instance.SetBattleResult(IsAllKill(), isFast, isVeryFast, Mathf.FloorToInt(GameData.Instance.pDataInGame.pRemainTime));

            //
            Manager_Game.StopGameTimer();
            PlayScene.ChangeState(EPlayState.Finish);
            return;
        }

        //
        for (int i = 0; i < _spawnCoolDown.Length; i++)
        {
            Spawn(GetSpawnPointByIndex(i));

            if (_spawnCoolDown[i] > 0f)
            {
                _spawnCoolDown[i] -= Manager_Game.pGameDeltaTime;
            }
        }

        //
        if (Manager_Game.pIsStopGameTimer == false)
            Manager_Damage.Instance.Update();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool Spawn(ESpawnPoint spawnPoint)
    {
        //
        var GD = GameData.Instance;
        var spawnCount = 0;

        if (GD.pDataInGame._dicStageEnemies_SpawnPoint.ContainsKey(spawnPoint) == false)
        {
            return false;
        }

        //
        foreach (var item in GD.pDataInGame._dicStageEnemies_SpawnPoint[spawnPoint])
        {
            if (item.pIsSpawn == false && item.pSpawnTime <= (_100_CommonValues.GetItem(1000012).value_int - GameData.Instance.pDataInGame.pRemainTime) && _spawnCoolDown[GetIndexSpawnPoint(spawnPoint)] <= 0f)
            {
                _spawnCoolDown[GetIndexSpawnPoint(spawnPoint)] = _spawnCoolTime;
                GD.pDataInGame.SpawnCharacter(item);
                return true;
            }            
        }

        //
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    int GetIndexSpawnPoint(ESpawnPoint spawnPoint)
    {
        switch (spawnPoint)
        {
            case ESpawnPoint._1: return 0;
            case ESpawnPoint._2: return 1;
            case ESpawnPoint._3: return 2;
            case ESpawnPoint._4: return 3;
            case ESpawnPoint._5: return 4;
            case ESpawnPoint._6: return 5;
            case ESpawnPoint._7: return 6;
            case ESpawnPoint._8: return 7;
            default: return -1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    ESpawnPoint GetSpawnPointByIndex(int index)
    {
        switch (index)
        {
            case 0: return ESpawnPoint._1;
            case 1: return ESpawnPoint._2;
            case 2: return ESpawnPoint._3;
            case 3: return ESpawnPoint._4;
            case 4: return ESpawnPoint._5;
            case 5: return ESpawnPoint._6;
            case 6: return ESpawnPoint._7;
            case 7: return ESpawnPoint._8;
            default: return ESpawnPoint.None;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsAllKill()
    {
        //
        if (_monsterCount != GameData.Instance.pDataInGame.pEnemySpawnCount)
        {
            return false;
        }

        //        
        if (GameData.Instance.pDataInGame.pEnemies.Count > 0)
            return false;

        //
        return true;
    }
}
