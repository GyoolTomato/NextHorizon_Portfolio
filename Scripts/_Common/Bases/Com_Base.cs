using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public abstract class Com_Base : MonoBehaviour
{
    //
    GameData _gameData = null;


    /// <summary>
    /// 
    /// </summary>
    public virtual void Init()
    {
        _gameData ??= GameData.Instance;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Awake()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Refresh()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Tick()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Tick_Sec()
    {

    }
}
