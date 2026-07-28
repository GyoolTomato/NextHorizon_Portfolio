using UnityEngine;

//[RequireComponent(typeof(Animator))]

public class CharacterPlayer : MonoBehaviour
{
    //
    _102_Character _table = null;
    CharacterIdleChanger _idleChanger;
    CharacterFaceChanger _faceChanger;
    CharacterStats _stats;

    public _102_Character pTable => _table;
    public ECharacterState pCharacterState { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        var anim = GetComponent<Animator>();

        _idleChanger = new CharacterIdleChanger();
        _idleChanger.Init(anim);

        _faceChanger = new CharacterFaceChanger();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        _stats.Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeState(ECharacterState characterState)
    {
        //
        if (pCharacterState == characterState)
        {
            return;
        }

        //
        pCharacterState = characterState;

        var anim = ECharacterAnim.None;

        switch (pCharacterState)
        {
            case ECharacterState.Idle    : anim = ECharacterAnim.Idle;     break;
            case ECharacterState.Walking : anim = ECharacterAnim.Walking;  break;
            case ECharacterState.Running : anim = ECharacterAnim.Running;  break;
            case ECharacterState.Jumping : anim = ECharacterAnim.Jumping;  break;
            case ECharacterState.Attack  : anim = ECharacterAnim.Attack;   break;
            case ECharacterState.Die     : anim = ECharacterAnim.Die;      break;
            case ECharacterState.Ceremony: anim = ECharacterAnim.Ceremony; break;
        }

        _idleChanger.SetAnim(anim);
    }
}
