using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _101_Items
{
    public class Values
    {
        public int key { private set; get; }
        public EItemType type { private set; get; }
        public EGrade grade { private set; get; }
        public int name { private set; get; }
        public int desc { private set; get; }

        [JsonConstructor]
        public Values(int key,EItemType type,EGrade grade,int name,int desc)
        {
            this.key = key;
            this.type = type;
            this.grade = grade;
            this.name = name;
            this.desc = desc;
        }
    }

    public static _101_Items.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_101_Items.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_101_Items[key];
        else
            return null;
    }


    public static List<_101_Items.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_101_Items;
    }
}