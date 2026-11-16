using System;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    //
    [SerializeField] Transform _bar;

    //
    Character _character;


    /// <summary>
    /// 
    /// </summary>
    public void Init(Character character)
    {
        _character = character;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateStat()
    {
        //if (_bar.gameObject.activeSelf == false)
        //    _bar.gameObject.SetActive(_character.pComStats.pHp < _character.pComStats.pHpMax);

        _bar.localScale = new Vector3(_character.pComStats.pHp / Convert.ToSingle(_character.pComStats.pHpMax), 0f, 0f);
        //_value.text = _character.pComStats.pHp.ToString();
    }
}
