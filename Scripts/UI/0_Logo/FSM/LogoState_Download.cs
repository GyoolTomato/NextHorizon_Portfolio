using Data;
using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class LogoState_Download : LogoState
{
    //
    static bool _isInitAddressable = false;
    static bool _isInitTableLoader = false;

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
            if (_isInitAddressable == false)
            {
                _isInitAddressable = true;
                Manager_Addressable.Instance.Init();                
            }
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

            if (_isInitTableLoader == false)
            {
                _isInitTableLoader = true;
                TableDataLoader.Instance.Init();                
            }            

            LogoScene.ChangeState(ELogoState.LogIn);
        }
    }
}
