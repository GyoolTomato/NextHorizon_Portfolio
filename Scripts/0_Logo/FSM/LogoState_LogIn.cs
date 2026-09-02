using Firebase.Auth;
using UnityEngine;

public class LogoState_LogIn : LogoState,
    Observer.IObserver<Observer.LoginSucceededEvent>,
    Observer.IObserver<Observer.LoginResponseParsedEvent>,
    Observer.IObserver<Observer.NewUserRequiredEvent>
{
    private string _loginLocalId;
    private string _loginFirebaseUid;
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
        if (FirebaseAuth.DefaultInstance == null)
        {
            Debug.LogError("FirebaseAuth.DefaultInstance is null");
            return;
        }

        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            Debug.LogError("Firebase current user is not ready.");
            return;
        }

        string localId = ProgramSettings.Instance.GetLocalUserId();
        string firebaseUid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        _loginLocalId = localId;
        _loginFirebaseUid = firebaseUid;

        if (string.IsNullOrWhiteSpace(localId))
        {
            Debug.LogError("Local user ID is empty.");
            return;
        }

        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

        ServerAPI.Instance.Send_Login(
            localId,
            firebaseUid,
            success =>
            {
                // 로그인 응답 파싱과 후속 처리는 옵저버 이벤트에서 수행한다.
            },
            HandleRequestFailure);
    }

    public void OnEvent(Observer.LoginSucceededEvent message)
    {
        //
        ServerUserData user = message.User;
        Debug.Log($"로그인 성공: userId={user.id}, items={user.items?.Length ?? 0}, characters={user.characters?.Length ?? 0}");
        UserData data = new UserData
        {
            id = user.id,
            localId = user.localId,
            firebaseUid = user.firebaseUid,
            nickname = user.nickname,
            level = user.level
        };

        //
        GameData.Instance.Init();
        GameData.Instance.pPlayerInfo.Init(data);
        GameData.Instance.pDataInventory.Init(user.items, user.armors, user.weapons);
        GameData.Instance.pDataCharacter.Init(user.characters);

        //
        GameManager.ChangeGameScene();
    }

    public void OnEvent(Observer.NewUserRequiredEvent message)
    {
        Debug.Log($"신규 사용자 계정을 생성합니다: {message.LocalId}");
        CreateNewUser(
            message.LocalId,
            message.FirebaseUid,
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

    public void CreateNewUser(string localId, string firebaseUid, string nickname)
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

        ServerAPI.Instance.Send_CreateUser(
            localId,
            firebaseUid,
            nickname,
            success => { },
            HandleRequestFailure);
    }

    public void OnEvent(Observer.LoginResponseParsedEvent message)
    {
        if (message.Response.isNew)
        {
            Observer.ObserverTracker<Observer.NewUserRequiredEvent>.Instance.Broadcast(
                new Observer.NewUserRequiredEvent(_loginLocalId, _loginFirebaseUid));
            return;
        }

        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Broadcast(
            new Observer.LoginSucceededEvent(message.Response.user));
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
