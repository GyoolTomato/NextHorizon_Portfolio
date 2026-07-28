using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class LogoState : SceneState
{
    //
    ELogoState _state = ELogoState.None;

    //
    public ELogoState pState => _state;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoState"></param>
    public LogoState(ELogoState logoState)
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
 