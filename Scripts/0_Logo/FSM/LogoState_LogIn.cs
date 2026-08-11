using Firebase.Auth;
using Observer;
using UnityEngine;

public class LogoState_LogIn : LogoState,
    IObserver<LoginSucceededEvent>,
    IObserver<NewUserRequiredEvent>,
    IObserver<ServerRequestFailedEvent>
{
    public LogoState_LogIn(ELogoState state) : base(state)
    {
    }

    public override void Enter()
    {
        ObserverTracker<LoginSucceededEvent>.Instance.Subscribe(this);
        ObserverTracker<NewUserRequiredEvent>.Instance.Subscribe(this);
        ObserverTracker<ServerRequestFailedEvent>.Instance.Subscribe(this);

        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.Init();
    }

    public override void Exit()
    {
        ObserverTracker<LoginSucceededEvent>.Instance.Unsubscribe(this);
        ObserverTracker<NewUserRequiredEvent>.Instance.Unsubscribe(this);
        ObserverTracker<ServerRequestFailedEvent>.Instance.Unsubscribe(this);
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
            response =>
            {
                if (response.isNew)
                {
                    ObserverTracker<NewUserRequiredEvent>.Instance.Broadcast(
                        new NewUserRequiredEvent(localId, firebaseUid));
                    return;
                }

                ObserverTracker<LoginSucceededEvent>.Instance.Broadcast(
                    new LoginSucceededEvent(response.user));
            },
            error => ObserverTracker<ServerRequestFailedEvent>.Instance.Broadcast(
                new ServerRequestFailedEvent(error)));
    }

    public void OnEvent(LoginSucceededEvent message)
    {
        ServerUserData user = message.User;
        UserData data = new UserData
        {
            id = user.id,
            localId = user.localId,
            firebaseUid = user.firebaseUid,
            nickname = user.nickname,
            level = user.level
        };

        GlobalData.Instance.pDataPlayerInfo.Init(data);
        GameManager.ChangeGameScene();
    }

    public void OnEvent(NewUserRequiredEvent message)
    {
        Debug.Log($"신규 사용자 계정을 생성합니다: {message.LocalId}");

#if UNITY_EDITOR
        // 닉네임 입력 UI가 연결되기 전까지 에디터의 로컬 ID를 초기 닉네임으로 사용한다.
        CreateNewUser(message.LocalId, message.FirebaseUid, message.LocalId);
#else
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.pComLogin.SetState(panel.pComLogin.GetCurrentLogInType());
        Debug.Log("신규 사용자 닉네임 입력이 필요합니다.");
#endif
    }

    public void CreateNewUser(string localId, string firebaseUid, string nickname)
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

        ServerAPI.Instance.Send_CreateUser(
            localId,
            firebaseUid,
            nickname,
            user => ObserverTracker<LoginSucceededEvent>.Instance.Broadcast(
                new LoginSucceededEvent(user)),
            error => ObserverTracker<ServerRequestFailedEvent>.Instance.Broadcast(
                new ServerRequestFailedEvent(error)));
    }

    public void OnEvent(ServerRequestFailedEvent message)
    {
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        Manager_UI.Instance.ShowMessageBox(
            Manager_UI.Instance.GetTextSystem(9990005),
            Manager_UI.Instance.GetTextSystem(9990006),
            Panel_MessageBox.EType.OK,
            () => panel.pComLogin.SetState(panel.pComLogin.GetCurrentLogInType()));

        Debug.LogError($"User API request failed: {message.Error}");
    }
}
