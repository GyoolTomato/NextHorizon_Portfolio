using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _999_SystemText
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

    public static _999_SystemText.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_999_SystemText.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_999_SystemText[key];
        else
            return null;
    }


    public static List<_999_SystemText.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_999_SystemText;
    }
}