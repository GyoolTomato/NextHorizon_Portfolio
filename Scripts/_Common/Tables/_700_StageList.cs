using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _700_StageList
{
    public class Values
    {
        public int key { private set; get; }
        public EStage stage { private set; get; }
        public int subNum { private set; get; }
        public int title { private set; get; }
        public int desc { private set; get; }
        public int crystalCount { private set; get; }
        public int goldCount { private set; get; }
        public int armorGroup { private set; get; }
        public int weaponGroup { private set; get; }
        public int[] monsters { private set; get; }

        [JsonConstructor]
        public Values(int key,EStage stage,int subNum,int title,int desc,int crystalCount,int goldCount,int armorGroup,int weaponGroup,int[] monsters)
        {
            this.key = key;
            this.stage = stage;
            this.subNum = subNum;
            this.title = title;
            this.desc = desc;
            this.crystalCount = crystalCount;
            this.goldCount = goldCount;
            this.armorGroup = armorGroup;
            this.weaponGroup = weaponGroup;
            this.monsters = monsters;
        }
    }

    public static _700_StageList.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_700_StageList.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_700_StageList[key];
        else
            return null;
    }


    public static List<_700_StageList.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_700_StageList;
    }
}