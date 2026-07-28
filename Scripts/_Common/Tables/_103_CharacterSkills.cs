using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _103_CharacterSkills
{
    public class Values
    {
        public int key { private set; get; }
        public int title { private set; get; }
        public int description { private set; get; }
        public ESkillType type { private set; get; }
        public double parameter0 { private set; get; }
        public double parameter1 { private set; get; }
        public double parameter2 { private set; get; }
        public double parameter3 { private set; get; }
        public double parameter4 { private set; get; }
        public double parameter5 { private set; get; }
        public double parameter6 { private set; get; }
        public double parameter7 { private set; get; }
        public double parameter8 { private set; get; }
        public double parameter9 { private set; get; }

        [JsonConstructor]
        public Values(int key,int title,int description,ESkillType type,double parameter0,double parameter1,double parameter2,double parameter3,double parameter4,double parameter5,double parameter6,double parameter7,double parameter8,double parameter9)
        {
            this.key = key;
            this.title = title;
            this.description = description;
            this.type = type;
            this.parameter0 = parameter0;
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
            this.parameter3 = parameter3;
            this.parameter4 = parameter4;
            this.parameter5 = parameter5;
            this.parameter6 = parameter6;
            this.parameter7 = parameter7;
            this.parameter8 = parameter8;
            this.parameter9 = parameter9;
        }
    }

    public static _103_CharacterSkills.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_103_CharacterSkills.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_103_CharacterSkills[key];
        else
            return null;
    }


    public static List<_103_CharacterSkills.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_103_CharacterSkills;
    }
}