using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _701_Stage_1
{
    public class Values
    {
        public int key { private set; get; }
        public int level { private set; get; }
        public int expToNextLevel { private set; get; }
        public int totalExpRequired { private set; get; }

        [JsonConstructor]
        public Values(int key,int level,int expToNextLevel,int totalExpRequired)
        {
            this.key = key;
            this.level = level;
            this.expToNextLevel = expToNextLevel;
            this.totalExpRequired = totalExpRequired;
        }
    }

    public static _701_Stage_1.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_701_Stage_1.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_701_Stage_1[key];
        else
            return null;
    }


    public static List<_701_Stage_1.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_701_Stage_1;
    }
}