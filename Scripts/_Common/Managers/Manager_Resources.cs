using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Manager_Resources : Singleton<Manager_Resources>
{
    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spriteKey"></param>
    /// <returns></returns>
    public Sprite GetSprite(string spriteKey)
    {
        //
        var sprite = Manager_Addressable.Instance.GetSprite(spriteKey);
        if (sprite == null)
        {
            Debug.LogError("Manager_Resources.GetSprite() - Sprite is null. spriteKey: " + spriteKey);
        }

        //
        return sprite;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageType"></param>
    /// <param name="tableInfo"></param>
    /// <returns></returns>
    public Sprite GetCharacterSprite(ECharacterImageType imageType, string model)
    {
        //
        var key = imageType.ToString() + "_" + model;
        var sprite = GetSprite(key);
        if (sprite == null)
        {             
            Debug.LogError("Manager_Resources.GetCharacterSprite() - Sprite is null. key: " + key);
        }

        //
        return sprite;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageType"></param>
    /// <param name="tableInfo"></param>
    /// <returns></returns>
    public Sprite GetIconSprite(int modelKey)
    {
        //
        var key = "Icon_" + modelKey;
        var sprite = GetSprite(key);
        if (sprite == null)
        {
            Debug.LogError("Manager_Resources.GetSkillIconSprite() - Sprite is null. key: " + key);
        }

        //
        return sprite;
    }
}
