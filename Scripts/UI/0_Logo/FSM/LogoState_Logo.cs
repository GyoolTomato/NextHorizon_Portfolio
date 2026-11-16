using Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoState_Logo : LogoState
{
    //
    static bool _isInitSystemText = false;

    //
    public LogoState_Logo(ELogoState state) : base(state)
    {
        
    }

    //
    override public void Enter()
    {
        //
        if (_isInitSystemText == false)
        {
            var textSystem = Resources.Load<TextAsset>("Tables/_999_SystemText");
            if (textSystem == null)
            {
                Debug.LogError("Not found '_999_SystemText'");
            }
            else
            {
                var temp_999_TextSystem = JsonConvert.DeserializeObject<List<_999_SystemText.Values>>(textSystem.text);
                foreach (var item in temp_999_TextSystem)
                {
                    TableDataLoader.Instance._list_999_SystemText.Add(item);
                    TableDataLoader.Instance._dic_999_SystemText.Add(item.key, item);
                }
            }

            _isInitSystemText = true;
        }

        //
        var panel = Manager_UI.Instance.ShowPanel(EPanelType.Title) as Panel_Title;
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
}
