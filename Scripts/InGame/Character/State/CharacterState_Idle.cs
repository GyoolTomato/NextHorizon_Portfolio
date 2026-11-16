using Anima2D;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CharacterState_Idle : CharacterState
{
    //
    bool _isChanged;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public CharacterState_Idle(ECharacterState state, Character character) : base(state, character)
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
        _character.pComAnim.SetAnim(ECharacterAnim.Idle);

        //
        _isChanged = false;        
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
            var target = Manager_SelectTarget.Instance.GetEnemyNearest(_character);
            if (target == null)
            {
                return;
            }

            var distance = Vector3.Distance(_character.transform.position, GameData.Instance.pDataInGame.pPlayer.transform.position);
            if (distance > Convert.ToSingle(Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.AtkDistance)))
            {
                if (_character.pData.pAllyType != EAllyType.Ally)
                    _character.ChangeState(ECharacterState.Running);
            }
            else
            {
                if (_character.pComStats.pAtkCoolDown <= 0f)
                {
                    if (_isChanged == false)
                    {
                        _isChanged = true;

                        var direction = target.transform.position - _character.transform.position;
                        _character.pComAnim.SetDirection(direction.x);

                        _character.pComStats.pAtkCoolDown = Convert.ToSingle(Manager_Character.Instance.GetTotalStat(_character, ECharacterStats.AtkCoolTime));
                        _character.DoAttack(target);                        
                    }
                }
            }
        }
    }
}
