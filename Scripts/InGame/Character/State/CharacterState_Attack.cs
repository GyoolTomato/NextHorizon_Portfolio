using System;
using System.Collections.Generic;
using System.Text;

public class CharacterState_Attack : CharacterState
{
    //
    bool _isChanged;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public CharacterState_Attack(ECharacterState state, Character character) : base(state, character)
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
        _character.pComAnim.SetAnim(ECharacterAnim.Attack);

        //
        _isChanged = false;
        _character.pComStats.pAtkDelayCoolDown = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.AtkDelay));
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
        if (_character != null)
        {
            if (_character.pComStats.pAtkDelayCoolDown > 0f)
            {
                _character.pComStats.pAtkDelayCoolDown -= Manager_Game.pGameDeltaTime;
            }
            else
            {
                if (_isChanged == false)
                {
                    _isChanged = true;
                    _character.ChangeState(ECharacterState.Idle);
                }
            }
        }
    }
}
