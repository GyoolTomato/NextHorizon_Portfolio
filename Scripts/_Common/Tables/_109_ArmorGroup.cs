using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _109_ArmorGroup
{
    public class Values
    {
        public int key { private set; get; }
        public int[] rewardKey { private set; get; }

        [JsonConstructor]
        public Values(int key,int[] rewardKey)
        {
            this.key = key;
            this.rewardKey = rewardKey;
        }
    }

    public static _109_ArmorGroup.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_109_ArmorGroup.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_109_ArmorGroup[key];
        else
            return null;
    }


    public static List<_109_ArmorGroup.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_109_ArmorGroup;
    }
}