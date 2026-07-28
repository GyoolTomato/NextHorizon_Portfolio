using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _800_Notice
{
    public class Values
    {
        public int key { private set; get; }
        public string start { private set; get; }
        public string end { private set; get; }
        public int sort { private set; get; }
        public int title { private set; get; }
        public int content { private set; get; }

        [JsonConstructor]
        public Values(int key,string start,string end,int sort,int title,int content)
        {
            this.key = key;
            this.start = start;
            this.end = end;
            this.sort = sort;
            this.title = title;
            this.content = content;
        }
    }

    public static _800_Notice.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_800_Notice.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_800_Notice[key];
        else
            return null;
    }


    public static List<_800_Notice.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_800_Notice;
    }
}