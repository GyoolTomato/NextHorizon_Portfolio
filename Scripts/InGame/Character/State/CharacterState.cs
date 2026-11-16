using System;
using System.Collections.Generic;
using System.Text;

public class CharacterState
{
    //
    protected ECharacterState _state = ECharacterState.None;
    protected Character _character;

    //
    public ECharacterState pState => _state;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoState"></param>
    public CharacterState(ECharacterState state, Character character)
    {
        _state = state;
        _character = character;
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
