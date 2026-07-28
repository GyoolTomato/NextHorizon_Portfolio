using Data;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Networking;
using MEC;
using System.Text;

public class LogoState_LogIn : LogoState
{
    //
    StringBuilder _sb = new StringBuilder();

    //
    public LogoState_LogIn(ELogoState state) : base(state)
    {
        
    }

    //
    override public void Enter()
    {
        //
        _sb ??= new StringBuilder();

        //
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.Init();
    }

    //
    override public void Exit()
    {

    }

    //
    override public void Update()
    {
        
    }

    //
    public void DoLoadUserData()
    {
        //
        if (FirebaseAuth.DefaultInstance == null)
        {
            Debug.LogError("FirebaseAuth.DefaultInstance is null");
            return;
        }

        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            Debug.LogError("CurrentUser is null. 아직 로그인 정보가 준비되지 않음");
            return;
        }

        //          
        var uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        //
        Timing.RunCoroutine(CoLoadUserData(uid));
    }

    IEnumerator<float> CoLoadUserData(string uid)
    {
        //
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;

        //
        string url = $"{ProgramSettings.Instance.pServerAddress}/user?uid={uid}";
        
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.SendWebRequest();
            panel.pComLogin.SetState(Com_Title_Login.EState.Loading);

            while (req.isDone == false)
            {
                yield return Timing.WaitForOneFrame;
            }

            if (req.result != UnityWebRequest.Result.Success)
            {
                Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextSystem(9990005), Manager_UI.Instance.GetTextSystem(9990006), Panel_MessageBox.EType.OK, ()=>
                {
                    panel.pComLogin.SetState(panel.pComLogin.GetCurrentLogInType());
                });
                Debug.LogError("유저 데이터 요청 실패: " + req.error);                
                yield break;
            }

            string json = req.downloadHandler.text;
            Debug.Log("응답 JSON: " + json);

            UserData data = JsonUtility.FromJson<UserData>(json);
            GlobalData.Instance.pDataPlayerInfo.Init(data);
            GameManager.ChangeGameScene();
        }
    }
}
