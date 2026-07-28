using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _104_Missions
{
    public class Values
    {
        public int key { private set; get; }
        public int title { private set; get; }
        public EMissionType type { private set; get; }
        public int count { private set; get; }
        public int[] rewardKeys { private set; get; }
        public long[] rewardCounts { private set; get; }

        [JsonConstructor]
        public Values(int key,int title,EMissionType type,int count,int[] rewardKeys,long[] rewardCounts)
        {
            this.key = key;
            this.title = title;
            this.type = type;
            this.count = count;
            this.rewardKeys = rewardKeys;
            this.rewardCounts = rewardCounts;
        }
    }

    public static _104_Missions.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_104_Missions.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_104_Missions[key];
        else
            return null;
    }


    public static List<_104_Missions.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_104_Missions;
    }
}