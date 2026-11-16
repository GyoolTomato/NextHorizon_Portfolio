using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CharacterState_Run : CharacterState
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public CharacterState_Run(ECharacterState state, Character character) : base(state, character)
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
        _character.pComAnim.SetAnim(ECharacterAnim.Running);
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
        if (_character.pData.pAllyType != EAllyType.Ally)
        {
            var distance = Vector3.Distance(_character.transform.position, GameData.Instance.pDataInGame.pPlayer.transform.position);
            if (distance <= Convert.ToSingle(Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.AtkDistance)))
            {
                _character.ChangeState(ECharacterState.Idle);
            }
            else
            {
                var direction = GameData.Instance.pDataInGame.pPlayer.transform.position - _character.transform.position;

                _character.pComAnim.SetDirection(direction.x);

                _character.transform.position += direction.normalized * Convert.ToSingle(Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.Speed)) * Manager_Game.pGameDeltaTime;
            }
        }
    }
}
