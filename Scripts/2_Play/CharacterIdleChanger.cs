using UnityEngine;
using System.Collections;
using TMPro;

//
public class CharacterIdleChanger
{
    //
    private Animator _anim;                     
    private AnimatorStateInfo _currentState;     
    public bool _random = false;                
    public float _threshold = 0.5f;             
    public float _interval = 10f;               
                                                
    public bool isGUI = true;

    ECharacterAnim _curAnim = ECharacterAnim.None;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="anim"></param>
    public void Init(Animator anim)
    {
        _anim = anim;

        _currentState = _anim.GetCurrentAnimatorStateInfo(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterAnim"></param>
    public void SetAnim(ECharacterAnim characterAnim, bool isForced = false)
    {
        //
        if (_curAnim == characterAnim && isForced ==  false)
        {
            return;
        }

        //
        _anim.Rebind();

        _curAnim = characterAnim;
        switch (_curAnim)
        {
            case ECharacterAnim.Idle:
                _anim.Play("Standing@loop");
                break;
            case ECharacterAnim.Attack:
                _anim.Play("Standing@loop");
                break;
            case ECharacterAnim.Walking:
                _anim.Play("Walking@loop");
                break;
            case ECharacterAnim.Running:
                _anim.Play("Running@loop");
                break;
            case ECharacterAnim.Jumping:
                _anim.Play("Jumping@loop");
                break;
            case ECharacterAnim.Die:
                _anim.Play("Standing@loop");
                break;
            case ECharacterAnim.Ceremony:
                _anim.Play("Standing@loop");
                break;
        }
    }
}
