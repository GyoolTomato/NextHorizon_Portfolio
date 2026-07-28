using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _105_Armors
{
    public class Values
    {
        public int key { private set; get; }
        public EGrade grade { private set; get; }
        public int name { private set; get; }
        public string icon { private set; get; }
        public EArmorType type { private set; get; }
        public int value { private set; get; }

        [JsonConstructor]
        public Values(int key,EGrade grade,int name,string icon,EArmorType type,int value)
        {
            this.key = key;
            this.grade = grade;
            this.name = name;
            this.icon = icon;
            this.type = type;
            this.value = value;
        }
    }

    public static _105_Armors.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_105_Armors.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_105_Armors[key];
        else
            return null;
    }


    public static List<_105_Armors.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_105_Armors;
    }
}