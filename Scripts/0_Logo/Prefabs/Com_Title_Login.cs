using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_Title_Login : Com_Base
{
    //
    [SerializeField] GameObject _btnLogInGoogle;
    [SerializeField] GameObject _btnLogInGuest;
    [SerializeField] GameObject _btnLogOut;
    [SerializeField] TextMeshProUGUI _txtLogInType;
    [SerializeField] GameObject _btnMessageForPlayGame;
    [SerializeField] TextMeshProUGUI _txtMessage;

    //
    public enum EState
    {
        None,
        LogIn_Google,
        LogIn_Guest,
        Loading,
    }

    //
    EState _state = EState.None;

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        Debug.Log("Init start");

        _btnLogInGoogle.SetActive(false);
        _btnLogInGuest.SetActive(true);
        _btnLogOut.SetActive(false);
        _txtLogInType.gameObject.SetActive(false);
        _btnMessageForPlayGame.SetActive(false);
        _txtMessage.gameObject.SetActive(false);

        //
        SetState(GetCurrentLogInType());
        Manager_Account.Instance.Init(success =>
        {
            if (success == false)
            {
                return;
            }

            SetState(GetCurrentLogInType());
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public EState GetCurrentLogInType()
    {
        switch (Manager_Account.Instance.pLoginType)
        {
            case Manager_Account.ELoginType.Guest:
                return EState.LogIn_Guest;
            case Manager_Account.ELoginType.Google:
                return EState.LogIn_Google;
            default:
                return EState.None;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void SetState(EState state)
    {
        _state = state;

        RefreshState(_state);
    }

    /// <summary>
    /// 
    /// </summary>
    void RefreshState(EState state)
    {
        _btnLogInGoogle         .SetActive(state == EState.None);
        _btnLogInGuest          .SetActive(state == EState.None);
        _btnLogOut              .SetActive(state == EState.LogIn_Google || state == EState.LogIn_Guest);
        _txtLogInType.gameObject.SetActive(state != EState.None);
        _btnMessageForPlayGame  .SetActive(state != EState.None && state != EState.Loading);
        _txtMessage  .gameObject.SetActive(state != EState.None);

        if (_txtLogInType.gameObject.activeSelf)
        {
            EState loginState = state == EState.Loading ? GetCurrentLogInType() : state;
            _txtLogInType.text = loginState == EState.LogIn_Guest ? "Guest Log In" : "Google Log In";
        }
        
        if (_txtMessage.gameObject.activeSelf)
        {
            _txtMessage.text = state != EState.Loading ? "Press Touch For Play Game" : "Loading...";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnGoogleLogin()
    {
        //
        if (_state != EState.None)
        {
            return;
        }

        //
        Manager_Account.Instance.LogInGoogle(success =>
        {
            if (success)
            {
                SetState(GetCurrentLogInType());
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnGuestLogin()
    {
        //
        if (_state != EState.None)
        {
            return;
        }

        //
        Manager_Account.Instance.LogInGuest();
        SetState(EState.LogIn_Guest);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLogOut()
    {
        //
        if (_state == EState.None || _state == EState.Loading)
        {
            return;
        }

        //
        Manager_Account.Instance.LogOut(false);
        SetState(EState.None);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnStartGame()
    {
        //
        if (_state == EState.None || _state == EState.Loading)
        {
            return;
        }

        //
        LogoScene.pStateLogIn.DoLogin();
    }
}
