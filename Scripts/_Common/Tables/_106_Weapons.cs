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
        public string icon { private set; get; }
        public int value { private set; get; }

        [JsonConstructor]
        public Values(int key,EGrade grade,int name,string icon,int value)
        {
            this.key = key;
            this.grade = grade;
            this.name = name;
            this.icon = icon;
            this.value = value;
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