using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    //
    [SerializeField] Collider _collider;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _pivotAnim;
    [SerializeField] Transform _attackPoint;
    [SerializeField] StatusBar _statusBar;

    //
    CharacterState_Idle _stateIdle;
    CharacterState_Run _stateRun;
    CharacterState_Attack _stateAttack;
    CharacterState_Die _stateDie;
    CharacterState_Skill _stateSkill;

    //
    CharacterAnim _comAnim;
    CharacterStats _comStats;

    //
    public CharacterAnim pComAnim => _comAnim;
    public CharacterStats pComStats => _comStats;
    public CharacterState pCurState { private set; get; }

    public DataInGameCharacter pData { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    public Character()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(DataInGameCharacter data)
    {
        //
        _stateIdle = new CharacterState_Idle(ECharacterState.Idle, this);
        _stateRun = new CharacterState_Run(ECharacterState.Running, this);
        _stateAttack = new CharacterState_Attack(ECharacterState.Attack, this);
        _stateDie = new CharacterState_Die(ECharacterState.Die, this);
        _stateSkill = new CharacterState_Skill(ECharacterState.Skill, this);

        //
        pData = data;

        //
        _comAnim = new CharacterAnim(_animator, _pivotAnim);
        _comStats = new CharacterStats(this);
        _pivotAnim.rotation= Manager_Playing.Instance.GetCameraTransform().rotation;
        _statusBar.Init(this);

        //
        ChangeState(ECharacterState.Idle);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Release()
    {
        pCurState?.Exit();
        pCurState = null;

        pData = null;
        _comAnim = null;
        _comStats = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void ChangeState(ECharacterState state)
    {
        //
        if (pCurState != null && pCurState.pState == state)
        {
            return;
        }

        //
        pCurState?.Exit();

        switch (state)
        {
            case ECharacterState.Idle: pCurState = _stateIdle; break;
            case ECharacterState.Running: pCurState = _stateRun; break;
            case ECharacterState.Attack: pCurState = _stateAttack; break;
            case ECharacterState.Die: pCurState = _stateDie; break;
            case ECharacterState.Skill: pCurState = _stateSkill; break;
        }

        // ChangeState는 동기적으로 실행되므로 다음 Update보다 Enter가 먼저 끝난다.
        pCurState?.Enter();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Heal(int value)
    {
        _comStats.AddDamage(-value);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Damage(int value)
    {
        //
        _comStats.AddDamage(value);

        if (_comStats.IsDead())
            DoDie();

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool DoDie()
    {
        _comStats.SetCorpse(true);

        ChangeState(ECharacterState.Die);
                
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dead()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void DoAttack(Character target)
    {
        ChangeState(ECharacterState.Attack);

        var projectileObj = Manager_InGamePool.Instance.SpawnProjectile(pData.pData.pTableInfo.projectile, _attackPoint.position);
        var projectile = projectileObj.GetComponent<Projectile>();
        projectile.Init(this, target.transform.position);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void DoWalk()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void DoRun()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void DoIdle()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (_comStats == null)
            return;

        pCurState?.Update();

        if (_comStats == null)
            return;

        if (_comStats.pAtkCoolDown > 0f)
        {
            _comStats.pAtkCoolDown -= Manager_Game.pGameDeltaTime;
        }

        //
        _statusBar.UpdateStat();
    }
}
