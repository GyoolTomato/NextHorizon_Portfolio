using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataCharacter
{
    //
    public bool pIsActive { set; get; } = false;
    public int pGrade { set; get; } = 1;
    public int pStack { set; get; } = 0;
    public int pLevel { set; get; } = 1;
    public long pExp { set; get; } = 0;
    public int pActiveLv { set; get; } = 0;
    public int[] pPassiveLv { set; get; } = new int[3] { 0, 0, 0 };
    public int pCharm { set; get; } = 0;
    public _102_Character.Values pTableInfo { private set; get; } = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public DataCharacter(_102_Character.Values tableInfo)
    {
        pTableInfo = tableInfo;
    }
}

public class GameData_Character
{
    //
    public List<DataCharacter> pCharacters { private set; get; }
    public Dictionary<int, DataCharacter> pDicCharacters { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerPlayerCharacterData[] characters)
    {
        characters = characters.OrderBy(x => x.characterKey).ToArray();

        pCharacters ??= new List<DataCharacter>();
        pDicCharacters ??= new Dictionary<int, DataCharacter>();

        pCharacters.Clear();
        pDicCharacters.Clear();

        foreach (var item in characters)
        {
            //
            var tableInfo = _102_Character.GetItem(item.characterKey);
            if (tableInfo == null)
            {
                Debug.LogError("Player Character Error : " + item.characterKey);
                continue;
            }

            //
            var temp = new DataCharacter(tableInfo)
            {
                pIsActive = true,
                pGrade = item.grade,
                pStack = item.stack,
                pLevel = item.level,
                pExp = item.exp,
                pActiveLv = item.activeLv,
                pCharm = item.charm
            };

            //
            pCharacters.Add(temp);
            pDicCharacters.Add(temp.pTableInfo.key, temp);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataCharacter GetCharacter(int id)
    {
        //
        if (pDicCharacters.ContainsKey(id))
            return pDicCharacters[id];

        //
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool AddCharacter(params int[] id)
    {
        //
        foreach (var i in id)
        {
            //
            var student = GetCharacter(i);
            if (student != null)
                continue;

            //
            if (student.pIsActive == false)
            {
                student.pIsActive = true;
            }
            else
            {
                student.pStack++;
            }
        }

        //
        return true;
    }
}
