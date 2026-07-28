using UnityEngine;

//[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    //
    [SerializeField] Transform _characterContainer = null;
    [SerializeField] CharacterPlayer _character = null;
    [SerializeField] Transform _front = null;

    Quaternion _quaternion = Quaternion.identity;

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        //
        var value = 0f;

        if (Input.GetKey("w"))
        {
            value = 0f;
        }

        if (Input.GetKey("s"))
        {
            value = 180f;
        }

        if (Input.GetKey("a"))
        {
            if (Input.GetKey("w"))
            {
                value += 315f;
            }
            else if (Input.GetKey("s"))
            {
                value += 45f;
            }
            else
            {
                value = 270f;
            }
        }

        if (Input.GetKey("d"))
        {
            if (Input.GetKey("w"))
            {
                value += 45f;
            }
            else if (Input.GetKey("s"))
            {
                value -= 45f;
            }
            else
            {
                value = 90f;
            }
        }

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            _character.ChangeState(ECharacterState.Walking);

            _quaternion = Quaternion.identity;
            _quaternion.eulerAngles = new Vector3(0f, value, 0f);
            _characterContainer.transform.rotation = _quaternion;

            var dir = (_front.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime;
        }

        //
        if (Input.GetKey("w") == false && Input.GetKey("s") == false && Input.GetKey("a") == false && Input.GetKey("d") == false)
        {
            _character.ChangeState(ECharacterState.Idle);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init(CharacterPlayer character)
    {
        SetCharacter(character);
    }

    public void SetCharacter(CharacterPlayer character)
    {
        _character = character;

        _character.transform.SetParent(transform);
    }


}
