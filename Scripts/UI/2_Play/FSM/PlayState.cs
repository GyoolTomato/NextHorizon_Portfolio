using System;
using System.Collections.Generic;
using System.Text;

public class PlayState : SceneState
{
    //
    EPlayState _state = EPlayState.None;

    //
    public EPlayState pState => _state;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoState"></param>
    public PlayState(EPlayState state)
    {
        _state = state;
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
