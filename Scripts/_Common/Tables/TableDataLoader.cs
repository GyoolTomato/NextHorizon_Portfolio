using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;

namespace Data
{
    public class TableDataLoader : Singleton<TableDataLoader>
    {
        public Dictionary<int, _100_CommonValues.Values> _dic_100_CommonValues = new Dictionary<int, _100_CommonValues.Values>();
        public List<_100_CommonValues.Values> _list_100_CommonValues = new List<_100_CommonValues.Values>();
        public Dictionary<int, _101_Items.Values> _dic_101_Items = new Dictionary<int, _101_Items.Values>();
        public List<_101_Items.Values> _list_101_Items = new List<_101_Items.Values>();
        public Dictionary<int, _102_Character.Values> _dic_102_Character = new Dictionary<int, _102_Character.Values>();
        public List<_102_Character.Values> _list_102_Character = new List<_102_Character.Values>();
        public Dictionary<int, _103_CharacterSkills.Values> _dic_103_CharacterSkills = new Dictionary<int, _103_CharacterSkills.Values>();
        public List<_103_CharacterSkills.Values> _list_103_CharacterSkills = new List<_103_CharacterSkills.Values>();
        public Dictionary<int, _104_Missions.Values> _dic_104_Missions = new Dictionary<int, _104_Missions.Values>();
        public List<_104_Missions.Values> _list_104_Missions = new List<_104_Missions.Values>();
        public Dictionary<int, _105_Armors.Values> _dic_105_Armors = new Dictionary<int, _105_Armors.Values>();
        public List<_105_Armors.Values> _list_105_Armors = new List<_105_Armors.Values>();
        public Dictionary<int, _106_Weapons.Values> _dic_106_Weapons = new Dictionary<int, _106_Weapons.Values>();
        public List<_106_Weapons.Values> _list_106_Weapons = new List<_106_Weapons.Values>();
        public Dictionary<int, _107_CharacterLevel.Values> _dic_107_CharacterLevel = new Dictionary<int, _107_CharacterLevel.Values>();
        public List<_107_CharacterLevel.Values> _list_107_CharacterLevel = new List<_107_CharacterLevel.Values>();
        public Dictionary<int, _800_Notice.Values> _dic_800_Notice = new Dictionary<int, _800_Notice.Values>();
        public List<_800_Notice.Values> _list_800_Notice = new List<_800_Notice.Values>();
        public Dictionary<int, _900_CommonText.Values> _dic_900_CommonText = new Dictionary<int, _900_CommonText.Values>();
        public List<_900_CommonText.Values> _list_900_CommonText = new List<_900_CommonText.Values>();
        public Dictionary<int, _901_CharacterText.Values> _dic_901_CharacterText = new Dictionary<int, _901_CharacterText.Values>();
        public List<_901_CharacterText.Values> _list_901_CharacterText = new List<_901_CharacterText.Values>();
        public Dictionary<int, _902_NoticeText.Values> _dic_902_NoticeText = new Dictionary<int, _902_NoticeText.Values>();
        public List<_902_NoticeText.Values> _list_902_NoticeText = new List<_902_NoticeText.Values>();
        public Dictionary<int, _903_MissionsText.Values> _dic_903_MissionsText = new Dictionary<int, _903_MissionsText.Values>();
        public List<_903_MissionsText.Values> _list_903_MissionsText = new List<_903_MissionsText.Values>();
        public Dictionary<int, _904_ItemsText.Values> _dic_904_ItemsText = new Dictionary<int, _904_ItemsText.Values>();
        public List<_904_ItemsText.Values> _list_904_ItemsText = new List<_904_ItemsText.Values>();
        public Dictionary<int, _905_SkillsText.Values> _dic_905_SkillsText = new Dictionary<int, _905_SkillsText.Values>();
        public List<_905_SkillsText.Values> _list_905_SkillsText = new List<_905_SkillsText.Values>();
        public Dictionary<int, _906_ArmorsText.Values> _dic_906_ArmorsText = new Dictionary<int, _906_ArmorsText.Values>();
        public List<_906_ArmorsText.Values> _list_906_ArmorsText = new List<_906_ArmorsText.Values>();
        public Dictionary<int, _907_WeaponsText.Values> _dic_907_WeaponsText = new Dictionary<int, _907_WeaponsText.Values>();
        public List<_907_WeaponsText.Values> _list_907_WeaponsText = new List<_907_WeaponsText.Values>();
        public Dictionary<int, _999_SystemText.Values> _dic_999_SystemText = new Dictionary<int, _999_SystemText.Values>();
        public List<_999_SystemText.Values> _list_999_SystemText = new List<_999_SystemText.Values>();


        public void Init()
        {
            var temp_100_CommonValues = JsonConvert.DeserializeObject<List<_100_CommonValues.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_100_CommonValues.bytes").text);
            foreach (var item in temp_100_CommonValues)
            {
                _list_100_CommonValues.Add(item);
                _dic_100_CommonValues.Add(item.key, item);
            }
            var temp_101_Items = JsonConvert.DeserializeObject<List<_101_Items.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_101_Items.bytes").text);
            foreach (var item in temp_101_Items)
            {
                _list_101_Items.Add(item);
                _dic_101_Items.Add(item.key, item);
            }
            var temp_102_Character = JsonConvert.DeserializeObject<List<_102_Character.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_102_Character.bytes").text);
            foreach (var item in temp_102_Character)
            {
                _list_102_Character.Add(item);
                _dic_102_Character.Add(item.key, item);
            }
            var temp_103_CharacterSkills = JsonConvert.DeserializeObject<List<_103_CharacterSkills.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_103_CharacterSkills.bytes").text);
            foreach (var item in temp_103_CharacterSkills)
            {
                _list_103_CharacterSkills.Add(item);
                _dic_103_CharacterSkills.Add(item.key, item);
            }
            var temp_104_Missions = JsonConvert.DeserializeObject<List<_104_Missions.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_104_Missions.bytes").text);
            foreach (var item in temp_104_Missions)
            {
                _list_104_Missions.Add(item);
                _dic_104_Missions.Add(item.key, item);
            }
            var temp_105_Armors = JsonConvert.DeserializeObject<List<_105_Armors.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_105_Armors.bytes").text);
            foreach (var item in temp_105_Armors)
            {
                _list_105_Armors.Add(item);
                _dic_105_Armors.Add(item.key, item);
            }
            var temp_106_Weapons = JsonConvert.DeserializeObject<List<_106_Weapons.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_106_Weapons.bytes").text);
            foreach (var item in temp_106_Weapons)
            {
                _list_106_Weapons.Add(item);
                _dic_106_Weapons.Add(item.key, item);
            }
            var temp_107_CharacterLevel = JsonConvert.DeserializeObject<List<_107_CharacterLevel.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_107_CharacterLevel.bytes").text);
            foreach (var item in temp_107_CharacterLevel)
            {
                _list_107_CharacterLevel.Add(item);
                _dic_107_CharacterLevel.Add(item.key, item);
            }
            var temp_800_Notice = JsonConvert.DeserializeObject<List<_800_Notice.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_800_Notice.bytes").text);
            foreach (var item in temp_800_Notice)
            {
                _list_800_Notice.Add(item);
                _dic_800_Notice.Add(item.key, item);
            }
            var temp_900_CommonText = JsonConvert.DeserializeObject<List<_900_CommonText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_900_CommonText.bytes").text);
            foreach (var item in temp_900_CommonText)
            {
                _list_900_CommonText.Add(item);
                _dic_900_CommonText.Add(item.key, item);
            }
            var temp_901_CharacterText = JsonConvert.DeserializeObject<List<_901_CharacterText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_901_CharacterText.bytes").text);
            foreach (var item in temp_901_CharacterText)
            {
                _list_901_CharacterText.Add(item);
                _dic_901_CharacterText.Add(item.key, item);
            }
            var temp_902_NoticeText = JsonConvert.DeserializeObject<List<_902_NoticeText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_902_NoticeText.bytes").text);
            foreach (var item in temp_902_NoticeText)
            {
                _list_902_NoticeText.Add(item);
                _dic_902_NoticeText.Add(item.key, item);
            }
            var temp_903_MissionsText = JsonConvert.DeserializeObject<List<_903_MissionsText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_903_MissionsText.bytes").text);
            foreach (var item in temp_903_MissionsText)
            {
                _list_903_MissionsText.Add(item);
                _dic_903_MissionsText.Add(item.key, item);
            }
            var temp_904_ItemsText = JsonConvert.DeserializeObject<List<_904_ItemsText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_904_ItemsText.bytes").text);
            foreach (var item in temp_904_ItemsText)
            {
                _list_904_ItemsText.Add(item);
                _dic_904_ItemsText.Add(item.key, item);
            }
            var temp_905_SkillsText = JsonConvert.DeserializeObject<List<_905_SkillsText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_905_SkillsText.bytes").text);
            foreach (var item in temp_905_SkillsText)
            {
                _list_905_SkillsText.Add(item);
                _dic_905_SkillsText.Add(item.key, item);
            }
            var temp_906_ArmorsText = JsonConvert.DeserializeObject<List<_906_ArmorsText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_906_ArmorsText.bytes").text);
            foreach (var item in temp_906_ArmorsText)
            {
                _list_906_ArmorsText.Add(item);
                _dic_906_ArmorsText.Add(item.key, item);
            }
            var temp_907_WeaponsText = JsonConvert.DeserializeObject<List<_907_WeaponsText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_907_WeaponsText.bytes").text);
            foreach (var item in temp_907_WeaponsText)
            {
                _list_907_WeaponsText.Add(item);
                _dic_907_WeaponsText.Add(item.key, item);
            }
        }

    }
}