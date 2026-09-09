using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

public class LogoState_LogIn : LogoState,
    Observer.IObserver<Observer.LoginSucceededEvent>,
    Observer.IObserver<Observer.LoginResponseParsedEvent>,
    Observer.IObserver<Observer.NewUserRequiredEvent>
{
    private string _loginLocalId;
    private string _loginFirebaseToken;
    private bool _isGuestLogin;
    public LogoState_LogIn(ELogoState state) : base(state)
    {
    }

    public override void Enter()
    {
        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Subscribe(this);
        Observer.ObserverTracker<Observer.LoginResponseParsedEvent>.Instance.Subscribe(this);
        Observer.ObserverTracker<Observer.NewUserRequiredEvent>.Instance.Subscribe(this);

        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.Init();
    }

    public override void Exit()
    {
        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Unsubscribe(this);
        Observer.ObserverTracker<Observer.LoginResponseParsedEvent>.Instance.Unsubscribe(this);
        Observer.ObserverTracker<Observer.NewUserRequiredEvent>.Instance.Unsubscribe(this);
    }

    public override void Update()
    {
    }

    public void DoLogin()
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        string localId = ProgramSettings.Instance.GetLocalUserId();
        _isGuestLogin = panel.pComLogin.GetCurrentLogInType() == Com_Title_Login.EState.LogIn_Guest;
        panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

        if (_isGuestLogin)
        {
            if (string.IsNullOrWhiteSpace(localId))
            {
                Debug.LogError("Local user ID is empty.");
                return;
            }
            _loginLocalId = localId;
            _loginFirebaseToken = null;
            ServerAPI.Instance.Send_GuestLogin(localId, success => { }, HandleRequestFailure);
            return;
        }

        FirebaseUser firebaseUser = FirebaseAuth.DefaultInstance?.CurrentUser;
        if (firebaseUser == null) { Debug.LogError("Firebase current user is not ready."); return; }
        firebaseUser.TokenAsync(false).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) { Debug.LogError("Firebase token request failed: " + task.Exception); return; }
            _loginLocalId = null;
            _loginFirebaseToken = task.Result;
            ServerAPI.Instance.Send_FirebaseLogin(_loginFirebaseToken, success => { }, HandleRequestFailure);
        });
    }

    public void OnEvent(Observer.LoginSucceededEvent message)
    {
        //
        ServerUserData user = message.User;
        ServerPlayerInfoData playerInfo = user.playerInfo;
        Debug.Log($"로그인 성공: uid={playerInfo.uid}, level={playerInfo.level}, exp={playerInfo.exp}");

        //
        GameData.Instance.Init();
        GameData.Instance.pPlayerInfo.Init(playerInfo);
        GameData.Instance.pDataInventory.Init(user.items, user.armors, user.weapons);
        GameData.Instance.pDataCharacter.Init(user.characters);
        GameData.Instance.pDataMissions.Init(user.missions);

        //
        GameManager.ChangeGameScene();
    }

    public void OnEvent(Observer.NewUserRequiredEvent message)
    {
        Debug.Log($"신규 사용자 계정을 생성합니다: {message.LocalId}");
        CreateNewUser(
            message.LocalId,
            CreateInitialNickname(message.LocalId));
    }

    private string CreateInitialNickname(string localId)
    {
        if (!string.IsNullOrWhiteSpace(localId) && localId.Length <= 16)
        {
            return localId;
        }

        string idPart = string.IsNullOrWhiteSpace(localId)
            ? System.Guid.NewGuid().ToString("N").Substring(0, 8)
            : localId.Substring(0, 8);

        return $"User{idPart}";
    }

    public void CreateNewUser(string localId, string nickname)
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

        if (_isGuestLogin)
            ServerAPI.Instance.Send_CreateGuest(localId, nickname, success => { }, HandleRequestFailure);
        else
            ServerAPI.Instance.Send_CreateFirebase(_loginFirebaseToken, nickname, success => { }, HandleRequestFailure);
    }

    public void OnEvent(Observer.LoginResponseParsedEvent message)
    {
        if (message.Data.isNew)
        {
            Observer.ObserverTracker<Observer.NewUserRequiredEvent>.Instance.Broadcast(
                new Observer.NewUserRequiredEvent(_loginLocalId));
            return;
        }

        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Broadcast(
            new Observer.LoginSucceededEvent(message.Data.user));
    }

    private void HandleRequestFailure(ServerAPIError error)
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        Manager_UI.Instance.ShowMessageBox(
            Manager_UI.Instance.GetTextSystem(9990005),
            Manager_UI.Instance.GetTextSystem(9990006),
            Panel_MessageBox.EType.OK,
            () => panel.pComLogin.SetState(panel.pComLogin.GetCurrentLogInType()));

        Debug.LogError($"User API request failed: status={error.statusCode}, message={error.message}");
    }
}
