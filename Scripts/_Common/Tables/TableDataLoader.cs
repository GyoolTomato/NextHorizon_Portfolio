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
        public Dictionary<int, _108_PlayerLevel.Values> _dic_108_PlayerLevel = new Dictionary<int, _108_PlayerLevel.Values>();
        public List<_108_PlayerLevel.Values> _list_108_PlayerLevel = new List<_108_PlayerLevel.Values>();
        public Dictionary<int, _109_ArmorGroup.Values> _dic_109_ArmorGroup = new Dictionary<int, _109_ArmorGroup.Values>();
        public List<_109_ArmorGroup.Values> _list_109_ArmorGroup = new List<_109_ArmorGroup.Values>();
        public Dictionary<int, _110_WeaponGroup.Values> _dic_110_WeaponGroup = new Dictionary<int, _110_WeaponGroup.Values>();
        public List<_110_WeaponGroup.Values> _list_110_WeaponGroup = new List<_110_WeaponGroup.Values>();
        public Dictionary<int, _111_WeaponLevel.Values> _dic_111_WeaponLevel = new Dictionary<int, _111_WeaponLevel.Values>();
        public List<_111_WeaponLevel.Values> _list_111_WeaponLevel = new List<_111_WeaponLevel.Values>();
        public Dictionary<int, _112_ArmorLevel.Values> _dic_112_ArmorLevel = new Dictionary<int, _112_ArmorLevel.Values>();
        public List<_112_ArmorLevel.Values> _list_112_ArmorLevel = new List<_112_ArmorLevel.Values>();
        public Dictionary<int, _700_StageList.Values> _dic_700_StageList = new Dictionary<int, _700_StageList.Values>();
        public List<_700_StageList.Values> _list_700_StageList = new List<_700_StageList.Values>();
        public Dictionary<int, _701_Stage_1.Values> _dic_701_Stage_1 = new Dictionary<int, _701_Stage_1.Values>();
        public List<_701_Stage_1.Values> _list_701_Stage_1 = new List<_701_Stage_1.Values>();
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
        public Dictionary<int, _908_StageText.Values> _dic_908_StageText = new Dictionary<int, _908_StageText.Values>();
        public List<_908_StageText.Values> _list_908_StageText = new List<_908_StageText.Values>();
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
            var temp_108_PlayerLevel = JsonConvert.DeserializeObject<List<_108_PlayerLevel.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_108_PlayerLevel.bytes").text);
            foreach (var item in temp_108_PlayerLevel)
            {
                _list_108_PlayerLevel.Add(item);
                _dic_108_PlayerLevel.Add(item.key, item);
            }
            var temp_109_ArmorGroup = JsonConvert.DeserializeObject<List<_109_ArmorGroup.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_109_ArmorGroup.bytes").text);
            foreach (var item in temp_109_ArmorGroup)
            {
                _list_109_ArmorGroup.Add(item);
                _dic_109_ArmorGroup.Add(item.key, item);
            }
            var temp_110_WeaponGroup = JsonConvert.DeserializeObject<List<_110_WeaponGroup.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_110_WeaponGroup.bytes").text);
            foreach (var item in temp_110_WeaponGroup)
            {
                _list_110_WeaponGroup.Add(item);
                _dic_110_WeaponGroup.Add(item.key, item);
            }
            var temp_111_WeaponLevel = JsonConvert.DeserializeObject<List<_111_WeaponLevel.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_111_WeaponLevel.bytes").text);
            foreach (var item in temp_111_WeaponLevel)
            {
                _list_111_WeaponLevel.Add(item);
                _dic_111_WeaponLevel.Add(item.key, item);
            }
            var temp_112_ArmorLevel = JsonConvert.DeserializeObject<List<_112_ArmorLevel.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_112_ArmorLevel.bytes").text);
            foreach (var item in temp_112_ArmorLevel)
            {
                _list_112_ArmorLevel.Add(item);
                _dic_112_ArmorLevel.Add(item.key, item);
            }
            var temp_700_StageList = JsonConvert.DeserializeObject<List<_700_StageList.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_700_StageList.bytes").text);
            foreach (var item in temp_700_StageList)
            {
                _list_700_StageList.Add(item);
                _dic_700_StageList.Add(item.key, item);
            }
            var temp_701_Stage_1 = JsonConvert.DeserializeObject<List<_701_Stage_1.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_701_Stage_1.bytes").text);
            foreach (var item in temp_701_Stage_1)
            {
                _list_701_Stage_1.Add(item);
                _dic_701_Stage_1.Add(item.key, item);
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
            var temp_908_StageText = JsonConvert.DeserializeObject<List<_908_StageText.Values>>(Manager_Addressable.Instance.GetTable("Assets/Tables/_908_StageText.bytes").text);
            foreach (var item in temp_908_StageText)
            {
                _list_908_StageText.Add(item);
                _dic_908_StageText.Add(item.key, item);
            }
        }

    }
}