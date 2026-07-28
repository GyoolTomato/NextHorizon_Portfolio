using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _102_Character
{
    public class Values
    {
        public int key { private set; get; }
        public int name { private set; get; }
        public string model { private set; get; }
        public EGrade grade { private set; get; }
        public double hp { private set; get; }
        public double hp_level { private set; get; }
        public double atk { private set; get; }
        public double atk_level { private set; get; }
        public double def { private set; get; }
        public double def_level { private set; get; }
        public double avoid { private set; get; }
        public double avoid_level { private set; get; }
        public double focus { private set; get; }
        public double focus_level { private set; get; }
        public double atkspd { private set; get; }
        public double atkspd_level { private set; get; }
        public double speed { private set; get; }
        public double crirate { private set; get; }
        public double crirate_level { private set; get; }
        public double cridmg { private set; get; }
        public double cridmg_level { private set; get; }
        public int activeSkill { private set; get; }
        public int passiveSkill_0 { private set; get; }
        public int passiveSkill_1 { private set; get; }

        [JsonConstructor]
        public Values(int key,int name,string model,EGrade grade,double hp,double hp_level,double atk,double atk_level,double def,double def_level,double avoid,double avoid_level,double focus,double focus_level,double atkspd,double atkspd_level,double speed,double crirate,double crirate_level,double cridmg,double cridmg_level,int activeSkill,int passiveSkill_0,int passiveSkill_1)
        {
            this.key = key;
            this.name = name;
            this.model = model;
            this.grade = grade;
            this.hp = hp;
            this.hp_level = hp_level;
            this.atk = atk;
            this.atk_level = atk_level;
            this.def = def;
            this.def_level = def_level;
            this.avoid = avoid;
            this.avoid_level = avoid_level;
            this.focus = focus;
            this.focus_level = focus_level;
            this.atkspd = atkspd;
            this.atkspd_level = atkspd_level;
            this.speed = speed;
            this.crirate = crirate;
            this.crirate_level = crirate_level;
            this.cridmg = cridmg;
            this.cridmg_level = cridmg_level;
            this.activeSkill = activeSkill;
            this.passiveSkill_0 = passiveSkill_0;
            this.passiveSkill_1 = passiveSkill_1;
        }
    }

    public static _102_Character.Values GetItem(int key)
    {
        if (Data.TableDataLoader.Instance._dic_102_Character.ContainsKey(key))
            return Data.TableDataLoader.Instance._dic_102_Character[key];
        else
            return null;
    }


    public static List<_102_Character.Values> GetList()
    {
        return Data.TableDataLoader.Instance._list_102_Character;
    }
}