using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _903_MissionsText
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

    public static _903_MissionsText.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_903_MissionsText.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_903_MissionsText[key];
        else
            return null;
    }


    public static List<_903_MissionsText.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_903_MissionsText;
    }
}