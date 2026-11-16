using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class Player : MonoBehaviour
{
    //
    [SerializeField] Camera _mainCamera;

    
    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Transform GetCameraTransform()
    {
        return _mainCamera.transform;
    }
}
