using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _110_WeaponGroup
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

    public static _110_WeaponGroup.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_110_WeaponGroup.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_110_WeaponGroup[key];
        else
            return null;
    }


    public static List<_110_WeaponGroup.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_110_WeaponGroup;
    }
}