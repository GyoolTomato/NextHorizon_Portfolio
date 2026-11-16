using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataInGameCharacter
{
    //
    public EAllyType pAllyType { private set; get; }
    public DataCharacter pData { private set; get; }
    public ESpawnPoint pSpawnPoint { private set; get; }
    public float pSpawnTime { private set; get; }
    public bool pIsSpawn { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="allyType"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="spawnTime"></param>
    public void Init(DataCharacter data, EAllyType allyType, ESpawnPoint spawnPoint, float spawnTime)
    {
        pData = data;
        pAllyType = allyType;
        pSpawnPoint = spawnPoint; 
        pSpawnTime = spawnTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSpawn"></param>
    public void SetIsSpawn(bool isSpawn)
    {
        pIsSpawn = isSpawn;
    }
}

/// <summary>
/// 
/// </summary>
public class GameData_InGame
{
    //
    public DataInGameCharacter pDataPlayer { private set; get; }
    public  DataStage pStageInfo { private set; get; }
    public Dictionary<ESpawnPoint, List<DataInGameCharacter>> _dicStageEnemies_SpawnPoint { private set; get; }

    //
    public Character pPlayer { private set; get; }
    public List<Character> pAllies { private set; get; }
    public List<Character> pEnemies { private set; get; }
    public List<Character> pNeuts { private set; get; }
    public float pRemainTime { set; get; }
    public int pEnemySpawnCount { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init(DataCharacter player, DataStage stageInfo, ESpawnPoint[] spawnPoint, float[] spawnTime, int[] count, int[] monster)
    {
        //
        pPlayer = null;
        pEnemySpawnCount = 0;

        //
        var temp = new DataInGameCharacter();
        temp.Init(player, EAllyType.Ally, ESpawnPoint._Player, 0f);
        pDataPlayer = temp;
        pStageInfo = stageInfo;

        //
        _dicStageEnemies_SpawnPoint ??= new Dictionary<ESpawnPoint, List<DataInGameCharacter>>();
        _dicStageEnemies_SpawnPoint.Clear();

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            //
            if (spawnPoint[i] == ESpawnPoint.None)
            {
                continue;
            }

            var tableInfo = _102_Character.GetItem(monster[i]);
            if (tableInfo == null)
            {
                continue;
            }

            //           
            if (_dicStageEnemies_SpawnPoint.ContainsKey(spawnPoint[i]) == false)
                _dicStageEnemies_SpawnPoint.Add(spawnPoint[i], new List<DataInGameCharacter>());                       

            //
            for (int j = 0; j < count[i]; j++)
            {
                temp = new DataInGameCharacter();
                var tempData = new DataCharacter(tableInfo);
                tempData.pLevel = stageInfo.pTableInfo.level + UnityEngine.Random.Range(0, stageInfo.pTableInfo.level_gap);

                temp.Init(tempData, EAllyType.Enemy, spawnPoint[i], spawnTime[i]);

                _dicStageEnemies_SpawnPoint[spawnPoint[i]].Add(temp);
            }            
        }

        //
        pAllies ??= new List<Character>();
        pAllies.Clear();
        pEnemies ??= new List<Character>();
        pEnemies.Clear();
        pNeuts ??= new List<Character>();
        pNeuts.Clear();
    }

    //
    public Character SpawnPlayer()
    {
        //
        var character = SpawnObject(pDataPlayer);

        //
        pPlayer = character;

        //
        pAllies.Add(pPlayer);
        pPlayer.transform.SetAsLastSibling();

        //
        return character;
    }

    /// <summary>
    /// 
    /// </summary>
    public Character SpawnCharacter(DataInGameCharacter data)
    {
        //
        var character = SpawnObject(data);

        //
        if (pPlayer != null)
        {
            switch (data.pAllyType)
            {
                case EAllyType.Ally: pAllies.Add(character); pPlayer.transform.SetAsLastSibling(); break;
                case EAllyType.Enemy: pEnemies.Add(character); pPlayer.transform.SetAsLastSibling(); pEnemySpawnCount++; break;
                case EAllyType.Neutrality: pNeuts.Add(character); pPlayer.transform.SetAsLastSibling(); break;
            }
        }

        return character;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Character SpawnObject(DataInGameCharacter data)
    {
        //
        var temp = Manager_InGamePool.Instance.SpawnCharacter(data.pData.pTableInfo.key, Manager_Playing.Instance.GetSpawnPoint(data.pSpawnPoint));

        var character = temp.GetComponent<Character>();
        if (character == null)
        {
            Debug.LogError("Not found 'Character' class");
            return null;
        }

        character.Init(data);
        data.SetIsSpawn(true);

        //
        return character;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool RemoveCharacter(Character character)
    {
        //
        if (character == null)
            return false;

        //
        if (Manager_InGamePool.Instance.RemoveCharacter(character.pData.pData.pTableInfo.key, character.gameObject))
        {
            switch (character.pData.pAllyType)
            {
                case EAllyType.Ally:
                    pAllies.Remove(character);
                    break;
                case EAllyType.Enemy:
                    pEnemies.Remove(character);
                    break;
                case EAllyType.Neutrality:
                    pNeuts.Remove(character);
                    break;
            }

            character.Release();

            return true;
        }

        //
        return false;
    }
}
