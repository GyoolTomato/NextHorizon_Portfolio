using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CharacterAnim
{
    //
    Animator _anim;
    Transform _pivot;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="anim"></param>
    public CharacterAnim(Animator anim, Transform pivot)
    {
        _anim = anim;
        _pivot = pivot;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetAnim(ECharacterAnim anim)
    {
        //
        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }

        //
        _anim.speed = 1f;

        //
        switch (anim)
        {
            case ECharacterAnim.Walking: _anim.Play("Walk"  ); break;
            case ECharacterAnim.Running: _anim.Play("Run"   ); break;
            case ECharacterAnim.Attack : _anim.Play("Attack"); break;
            case ECharacterAnim.Die    : _anim.Play("Die"   ); break;
            default                    : _anim.Play("Idle"  ); break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetDirection(float directionX)
    {
        var changePivot = directionX >= 0f ? 1 : -1;

        if (_pivot.localScale.x != changePivot)
        {
            _pivot.localScale = new Vector3(changePivot, 1, 1);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AnimatorStateInfo GetAnimStateInfo()
    {
        return _anim.GetCurrentAnimatorStateInfo(0);
    }

    public void OnAttackFinished()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void StopAnim()
    {
        _anim.speed = 0f;
    }
}
