using UnityEngine;
using System.Collections;
using System;

public class Com_Title_Logo : Com_Base, Observer.IObserver<Observer.VersionReceivedEvent>
{
    //
    bool _isDoingCheckVersion = false;
    bool _isFoundNewVersion = false;
    string _url = string.Empty;

    //
    public void OnEvent(Observer.VersionReceivedEvent msg)
    {
        //
        var currentVersion = new Version(Application.version);
        var serverVersion = new Version(msg.Version.nowVersion);

        //
        if (currentVersion < serverVersion)
        {
            //
            _isFoundNewVersion = true;
            _isDoingCheckVersion = false;
            
            //
            _url = msg.Version.downloadUrl;

            //
            Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextSystem(9990008), Manager_UI.Instance.GetTextSystem(9990009), Panel_MessageBox.EType.OK, () =>
            {
                StartCoroutine(OpenUrlAndQuit());
            });
        }
        else
        {
            _isDoingCheckVersion = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        Observer.ObserverTracker<Observer.VersionReceivedEvent>.Instance.Subscribe(this);

        //
        _isDoingCheckVersion = true;

        //
        ServerAPI.Instance.Send_Version((success) =>
        {
            
        },
       (error) =>
       {
           Manager_UI.Instance.ShowMessageBox(Manager_UI.Instance.GetTextSystem(9990005), Manager_UI.Instance.GetTextSystem(9990010), Panel_MessageBox.EType.OK, () =>
           {
#if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
           });
       });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenUrlAndQuit()
    {
        //
        Application.OpenURL(_url);

        //
        yield return new WaitForSecondsRealtime(0.5f);

        //
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLogo()
    {
        //
        if (_isDoingCheckVersion || _isFoundNewVersion)
        {
            return;
        }

        //
        Observer.ObserverTracker<Observer.VersionReceivedEvent>.Instance.Unsubscribe(this);

        //
        LogoScene.ChangeState(ELogoState.Download);
    }
}
