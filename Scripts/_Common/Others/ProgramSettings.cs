using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/ItemData")]
class ProgramSettings : ScriptableObject
{
    //
    static ProgramSettings _instance = null;

    public static ProgramSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ProgramSettings>("ProgramSettings");
            }
            return _instance;
        }
    }

    //
    [SerializeField] private string ServerAddress;
    [SerializeField] private string DeviceUID;
    //[SerializeField] private bool IsLocalTestMode;

    //
    public string pServerAddress => ServerAddress;
    //public bool pIsLocalTestMode => IsLocalTestMode;


    /// <summary>
    /// 
    /// </summary>
    ProgramSettings()
    {

    }

    public string GetLocalUserId()
    {
#if UNITY_EDITOR
        if (!string.IsNullOrWhiteSpace(DeviceUID))
        {
            return DeviceUID.Trim();
        }

        return $"editor-{SystemInfo.deviceUniqueIdentifier}";
#else
        return SystemInfo.deviceUniqueIdentifier;
#endif
    }
}
