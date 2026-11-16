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
    [SerializeField] private bool IsTestServer;
    [SerializeField] private string TestServerAddress;
    [SerializeField] private string[] ServerAddress;
    [SerializeField] private string DeviceUID;
    //[SerializeField] private bool IsLocalTestMode;

    //
    public string[] pServerAddresses
    {
        get
        {
#if UNITY_EDITOR
            if (IsTestServer && !string.IsNullOrWhiteSpace(TestServerAddress))
            {
                return new[] { TestServerAddress };
            }
#endif
            return ServerAddress;
        }
    }
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

    public string GetServerAddress(int index)
    {
        string[] addresses = pServerAddresses;
        if (addresses == null || index < 0 || index >= addresses.Length)
        {
            return string.Empty;
        }

        return (addresses[index] ?? string.Empty).Trim().TrimEnd('/');
    }
}
