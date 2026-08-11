using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Manager_Table : Singleton<Manager_Table>
{
    //
    Dictionary<EItemType, _101_Items.Values> _dic_Items = new Dictionary<EItemType, _101_Items.Values>();
    Dictionary<int, _107_CharacterLevel.Values> _dic_Level_CharacterLevel = new Dictionary<int, _107_CharacterLevel.Values>();


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        foreach (var item in _101_Items.GetList())
        {
            //
            if (_dic_Items.ContainsKey(item.type))
            {
                Debug.LogError($"Manager_Table::Init() - Duplicate type key found: {item.type}");
                continue;
            }

            //
            _dic_Items.Add(item.type, item);
        }

        //
        foreach (var item in _107_CharacterLevel.GetList())
        {
            //
            if (_dic_Level_CharacterLevel.ContainsKey(item.level))
            {
                Debug.LogError($"Manager_Table::Init() - Duplicate level key found: {item.level}");
                continue;
            }

            //
            _dic_Level_CharacterLevel.Add(item.level, item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public _101_Items.Values GetItemInfo(EItemType type)
    {
        if (_dic_Items.ContainsKey(type))
        {
            return _dic_Items[type];
        }
        else
        {
            Debug.LogError($"Manager_Table::GetItemInfo() - Type info not found for type: {type}");
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public _107_CharacterLevel.Values GetCharacterLevelInfo(int level)
    {
        if (_dic_Level_CharacterLevel.ContainsKey(level))
        {
            return _dic_Level_CharacterLevel[level];
        }
        else
        {
            Debug.LogError($"Manager_Table::GetCharacterLevelInfo() - Level info not found for level: {level}");
            return null;
        }
    }
}
