using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _905_SkillsText
{
    public class Values
    {
        public int key { private set; get; }
        public string korean { private set; get; }
        public string english { private set; get; }
        public string japanese { private set; get; }

        [JsonConstructor]
        public Values(int key,string korean,string english,string japanese)
        {
            this.key = key;
            this.korean = korean;
            this.english = english;
            this.japanese = japanese;
        }
    }

    public static _905_SkillsText.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_905_SkillsText.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_905_SkillsText[key];
        else
            return null;
    }


    public static List<_905_SkillsText.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_905_SkillsText;
    }
}