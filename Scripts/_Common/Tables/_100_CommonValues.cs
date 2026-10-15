using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _100_CommonValues
{
    public class Values
    {
        public int key { private set; get; }
        public int value_int { private set; get; }
        public double value_double { private set; get; }
        public string value_text { private set; get; }

        [JsonConstructor]
        public Values(int key,int value_int,double value_double,string value_text)
        {
            this.key = key;
            this.value_int = value_int;
            this.value_double = value_double;
            this.value_text = value_text;
        }
    }

    public static _100_CommonValues.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_100_CommonValues.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_100_CommonValues[key];
        else
            return null;
    }


    public static List<_100_CommonValues.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_100_CommonValues;
    }
}