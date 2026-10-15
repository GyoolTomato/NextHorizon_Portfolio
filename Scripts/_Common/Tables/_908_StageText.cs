using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _908_StageText
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

    public static _908_StageText.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_908_StageText.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_908_StageText[key];
        else
            return null;
    }


    public static List<_908_StageText.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_908_StageText;
    }
}