using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData_Character
{
    //
    public List<Character> pCharacters { private set; get; }
    public Dictionary<int, Character> pDicCharacters { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerPlayerCharacterData[] characters)
    {
        characters = characters.OrderBy(x => x.characterKey).ToArray();

        pCharacters ??= new List<Character>();
        pDicCharacters ??= new Dictionary<int, Character>();

        pCharacters.Clear();
        pDicCharacters.Clear();

        var indexServerData = 0;
        foreach (var item in _102_Character.GetList())
        {
            var temp = new Character(item);

            var serverData = characters[indexServerData];
            if (temp.pTableInfo.key == serverData.characterKey)
            {
                temp.pIsActive = true;
                temp.pGrade = 0;
                temp.pStack = serverData.stack;
                temp.pLevel = serverData.level;
                temp.pExp = serverData.exp;
                temp.pActiveLv = 0;
                temp.pCharm = 0;                
            }

            pCharacters.Add(temp);
            pDicCharacters.Add(item.key, temp);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Character GetCharacter(int id)
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
