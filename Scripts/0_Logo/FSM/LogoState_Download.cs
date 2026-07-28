using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using MEC;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LogoState_Download : LogoState
{
    //
    bool _isDone = false;

    //
    public LogoState_Download(ELogoState state) : base(state)
    {
    }

    //
    override public void Enter()
    {
        //
        _isDone = false;

        //
        var panel = Manager_UI.Instance.GetPanel(EPanelType.Title) as Panel_Title;
        panel.Init();


        //
        try
        {
            Manager_Addressable.Instance.Init();

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in LogoState_Download: {e.Message}");
        }
    }

    //
    override public void Exit()
    {

    }

    //
    override public void Update()
    {
        if (_isDone == false && Manager_Addressable.Instance.pIsInit)
        {
            _isDone = true;

            LogoScene.ChangeState(ELogoState.LogIn);
        }
    }
}
