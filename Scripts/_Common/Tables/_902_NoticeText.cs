using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _902_NoticeText
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

    public static _902_NoticeText.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_902_NoticeText.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_902_NoticeText[key];
        else
            return null;
    }


    public static List<_902_NoticeText.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_902_NoticeText;
    }
}