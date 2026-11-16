using MEC;
using System;
using System.Collections.Generic;
using System.Text;

public class CharacterState_Die : CharacterState
{
    //
    float _deadTimer = 0f;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public CharacterState_Die(ECharacterState state, Character character) : base(state, character)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Enter()
    {
        //
        base.Enter();

        //
        _character.pComAnim.SetAnim(ECharacterAnim.Die);
        _deadTimer = 0f;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Exit()
    {
        //
        base.Exit();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Update()
    {
        //
        _deadTimer += Manager_Game.pGameDeltaTime;
        if (_deadTimer > 0.4f)
        {            
            GameData.Instance.pDataInGame.RemoveCharacter(_character);
        }
    }
}
