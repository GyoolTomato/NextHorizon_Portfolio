using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _112_ArmorLevel
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

    public static _112_ArmorLevel.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_112_ArmorLevel.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_112_ArmorLevel[key];
        else
            return null;
    }


    public static List<_112_ArmorLevel.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_112_ArmorLevel;
    }
}