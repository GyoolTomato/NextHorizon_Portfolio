using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Character
{
    //
    public List<Character> pCharacters { private set; get; }
    public Dictionary<int, Character> pDicCharacters { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        pCharacters ??= new List<Character>();
        pDicCharacters ??= new Dictionary<int, Character>();

        pCharacters.Clear();
        pDicCharacters.Clear();

        foreach (var item in _102_Character.GetList())
        {
            var temp = new Character(item);

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
