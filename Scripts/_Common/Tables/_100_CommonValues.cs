using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _100_CommonValues
{
    public class Values
    {
        public int key { private set; get; }
        public double value { private set; get; }

        [JsonConstructor]
        public Values(int key,double value)
        {
            this.key = key;
            this.value = value;
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