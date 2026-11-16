using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _701_StageMonster
{
    public class Values
    {
        public int key { private set; get; }
        public ESpawnPoint[] spawnPoint { private set; get; }
        public float[] timer { private set; get; }
        public int[] count { private set; get; }
        public int[] monster { private set; get; }

        [JsonConstructor]
        public Values(int key,ESpawnPoint[] spawnPoint,float[] timer,int[] count,int[] monster)
        {
            this.key = key;
            this.spawnPoint = spawnPoint;
            this.timer = timer;
            this.count = count;
            this.monster = monster;
        }
    }

    public static _701_StageMonster.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_701_StageMonster.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_701_StageMonster[key];
        else
            return null;
    }


    public static List<_701_StageMonster.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_701_StageMonster;
    }
}