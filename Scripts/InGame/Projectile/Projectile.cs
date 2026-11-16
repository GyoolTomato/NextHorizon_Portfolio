using UnityEngine;

public class Projectile : MonoBehaviour
{
    //
    [SerializeField] Collider _collider;
    [SerializeField] Transform _sprite;

    //
    Character _owner;
    _113_Projectile.Values _tableInfo;
    Vector3 _direction;
    float _remainingLifetime;


    /// <summary>
    /// 
    /// </summary>
    public void Init(Character owner, Vector3 target)
    {
        _owner = owner;

        _tableInfo = _113_Projectile.GetItem(_owner.pData.pData.pTableInfo.projectile);
        if (_tableInfo == null)
        {
            Debug.LogError("Projectile table data not found");
            return;
        }

        _remainingLifetime = _tableInfo.lifeTime;

        _direction = new Vector3(target.x, transform.position.y, target.z) - transform.position;
        _direction = _direction.normalized;

        var cameraTransform = Manager_Playing.Instance.GetCameraTransform();

        transform.rotation = cameraTransform.rotation;

        var viewDirection = cameraTransform.InverseTransformDirection(_direction);

        var angle = Mathf.Atan2(viewDirection.y, viewDirection.x) * Mathf.Rad2Deg;
        _sprite.localRotation = Quaternion.Euler(0f, 0f, angle + 180f);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        _owner = null;
        _tableInfo = null;
        _direction = Vector3.zero;
        _remainingLifetime = 0f;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Update()
    {
        if (_owner != null && _tableInfo != null && _direction != Vector3.zero)
        {
            transform.position += _direction * _tableInfo.speed * Manager_Game.pGameDeltaTime;

            _remainingLifetime -= Manager_Game.pGameDeltaTime;
            if (_remainingLifetime <= 0f)
            {
                Manager_InGamePool.Instance.RemoveProjectile(_tableInfo.key, gameObject);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //
        var target = other.GetComponentInParent<Character>();
        if (target == null || _owner == null || target == _owner)
            return;

        // 아군 / 발사자 / 사망한 대상 제외
        // 중복 명중 방지
        // target에 피해 전달
        // 탄환을 풀에 반환

        //
        if (target.pData.pAllyType == _owner.pData.pAllyType)
        {
            return;
        }

        //
        if (_tableInfo != null)
        {
            if (target.pData.pAllyType == EAllyType.Ally)
            {

                Debug.Log("aaaa");
            }

            Manager_Damage.Instance.AddDamage(_owner, target, 1f);
            Manager_InGamePool.Instance.RemoveProjectile(_tableInfo.key, gameObject);
        }
    }
}
