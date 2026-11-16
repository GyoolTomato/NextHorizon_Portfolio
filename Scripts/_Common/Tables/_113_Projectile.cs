using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _113_Projectile
{
    public class Values
    {
        public int key { private set; get; }
        public float speed { private set; get; }
        public float lifeTime { private set; get; }

        [JsonConstructor]
        public Values(int key,float speed,float lifeTime)
        {
            this.key = key;
            this.speed = speed;
            this.lifeTime = lifeTime;
        }
    }

    public static _113_Projectile.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_113_Projectile.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_113_Projectile[key];
        else
            return null;
    }


    public static List<_113_Projectile.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_113_Projectile;
    }
}