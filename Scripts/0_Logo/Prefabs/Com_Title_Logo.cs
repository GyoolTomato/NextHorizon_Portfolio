using UnityEngine;

public class Com_Title_Logo : Com_Base
{
    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLogo()
    {
        LogoScene.ChangeState(ELogoState.Download);
    }
}
