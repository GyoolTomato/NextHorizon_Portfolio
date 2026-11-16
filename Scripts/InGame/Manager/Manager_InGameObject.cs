using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Manager_InGamePool : Singleton<Manager_InGamePool>
{
    //
    readonly Dictionary<int, Stack<GameObject>> _dicCharacterPool = new Dictionary<int, Stack<GameObject>>();
    readonly Dictionary<int, Stack<GameObject>> _dicProjectilePool = new Dictionary<int, Stack<GameObject>>();


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        _dicCharacterPool.Clear();
        _dicProjectilePool.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    GameObject CreateCharacter(int key, Vector3 pos)
    {
        //
        GameObject loadObject = Manager_Addressable.Instance.GetCharacter(key);
        if (loadObject == null)
        {
            Debug.LogError($"Character prefab not found. key : {key}");
            return null;
        }

        //
        var createObject = GameObject.Instantiate(loadObject, pos, Quaternion.identity);
        if (createObject == null)
        {
            Debug.LogError("CreateCharacters Create Fail! key : " + key);
            return null;
        }

        //
        return createObject;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    GameObject CreateProjectile(int key, Vector3 pos)
    {
        //
        GameObject loadObject = Manager_Addressable.Instance.GetProjectile(key);
        if (loadObject == null)
        {
            Debug.LogError($"Projectile prefab not found. key : {key}");
            return null;
        }

        //
        var createObject = GameObject.Instantiate(loadObject, pos, Quaternion.identity);
        if (createObject == null)
        {
            Debug.LogError("CreateProjectile Create Fail! key : " + key);
            return null;
        }

        //
        return createObject;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnCharacter(int key, Vector3 pos)
    {
        //
        GameObject temp = GetPooledObject(_dicCharacterPool, key);
        temp ??= CreateCharacter(key, pos);
        if (temp == null)
            return null;

        temp.transform.SetPositionAndRotation(pos, Quaternion.identity);
        temp.SetActive(true);

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnProjectile(int key, Vector3 pos)
    {
        //
        GameObject temp = GetPooledObject(_dicProjectilePool, key);
        temp ??= CreateProjectile(key, pos);
        if (temp == null)
            return null;

        temp.transform.SetPositionAndRotation(pos, Quaternion.identity);
        temp.SetActive(true);

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public bool RemoveCharacter(int key, GameObject gbj)
    {
        return ReturnObject(_dicCharacterPool, key, gbj);
    }

    /// <summary>
    /// 탄환을 탄환 전용 풀에 반환합니다.
    /// </summary>
    public bool RemoveProjectile(int key, GameObject gbj)
    {
        return ReturnObject(_dicProjectilePool, key, gbj);
    }

    GameObject GetPooledObject(Dictionary<int, Stack<GameObject>> pool, int key)
    {
        if (pool.TryGetValue(key, out Stack<GameObject> stack) == false)
            return null;

        while (stack.Count > 0)
        {
            GameObject pooledObject = stack.Pop();
            if (pooledObject != null)
                return pooledObject;
        }

        return null;
    }

    bool ReturnObject(Dictionary<int, Stack<GameObject>> pool, int key, GameObject gbj)
    {
        if (gbj == null || gbj.activeSelf == false)
            return false;

        gbj.SetActive(false);
        gbj.transform.SetPositionAndRotation(new Vector3(0f, -1000f, 0f), Quaternion.identity);

        if (pool.TryGetValue(key, out Stack<GameObject> stack) == false)
        {
            stack = new Stack<GameObject>();
            pool.Add(key, stack);
        }

        stack.Push(gbj);
        return true;
    }
}
