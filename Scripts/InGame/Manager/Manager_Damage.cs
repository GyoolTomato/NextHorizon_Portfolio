using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Manager_Damage : Singleton<Manager_Damage>
{
    //
    Queue<DamageRequest> _damages = new Queue<DamageRequest>();

    struct DamageRequest
    {
        //
        public Character _target;
        public int _damage;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public DamageRequest(Character target, int damage)
        {
            _target = target;
            _damage = damage;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        _damages.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void AddDamage(Character owner, Character target, float damagePercent)
    {
        //
        var avoidValue = target.pComStats.pAvoid - owner.pComStats.pFocus;
        if (Manager_Calculate.Instance.IsPrize(avoidValue < 0 ? 0 : avoidValue))
        {
            return;
        }

        //
        var damage = (owner.pComStats.pAtk - target.pComStats.pDef) * damagePercent;

        //
        if (Manager_Calculate.Instance.IsPrize(owner.pComStats.pCriRate))
        {
            damage *= owner.pComStats.pCriDmg;
        }

        //
        _damages.Enqueue(new DamageRequest(target, Mathf.FloorToInt(damage)));
    }

    /// <summary>
    /// 
    /// </summary>
    public void Update()
    {
        if (_damages.Count > 0)
        {
            while (_damages.Count > 0)
            {
                //
                var data = _damages.Dequeue();

                //
                if (data._target == null)
                    continue;

                //
                if (data._target.pComStats.IsDead() == false)
                {
                    data._target.Damage(data._damage);
                }
            }
        }
    }
}
