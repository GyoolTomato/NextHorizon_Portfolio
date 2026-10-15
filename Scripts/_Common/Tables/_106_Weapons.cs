using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _106_Weapons
{
    public class Values
    {
        public int key { private set; get; }
        public EGrade grade { private set; get; }
        public int name { private set; get; }
        public int desc { private set; get; }
        public string icon { private set; get; }
        public ECharacterStats mainValueType { private set; get; }
        public int mainValue { private set; get; }
        public int mainValue_level { private set; get; }
        public int subValue { private set; get; }
        public int subValue_level { private set; get; }

        [JsonConstructor]
        public Values(int key,EGrade grade,int name,int desc,string icon,ECharacterStats mainValueType,int mainValue,int mainValue_level,int subValue,int subValue_level)
        {
            this.key = key;
            this.grade = grade;
            this.name = name;
            this.desc = desc;
            this.icon = icon;
            this.mainValueType = mainValueType;
            this.mainValue = mainValue;
            this.mainValue_level = mainValue_level;
            this.subValue = subValue;
            this.subValue_level = subValue_level;
        }
    }

    public static _106_Weapons.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_106_Weapons.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_106_Weapons[key];
        else
            return null;
    }


    public static List<_106_Weapons.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_106_Weapons;
    }
}