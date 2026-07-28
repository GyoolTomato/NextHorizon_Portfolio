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

    //
    public string pServerAddress => ServerAddress;


    /// <summary>
    /// 
    /// </summary>
    ProgramSettings()
    {

    }
}
