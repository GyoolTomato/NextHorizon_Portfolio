using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GameState : SceneState
{
    //
    EGameState _state = EGameState.None;

    //
    public EGameState pState => _state;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoState"></param>
    public GameState(EGameState logoState)
    {
        _state = logoState;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Exit() { }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Update() { }
}
