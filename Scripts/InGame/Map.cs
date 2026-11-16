using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint_Player;
    [SerializeField] Transform _spawnPoint_1;
    [SerializeField] Transform _spawnPoint_2;
    [SerializeField] Transform _spawnPoint_3;
    [SerializeField] Transform _spawnPoint_4;
    [SerializeField] Transform _spawnPoint_5;
    [SerializeField] Transform _spawnPoint_6;
    [SerializeField] Transform _spawnPoint_7;
    [SerializeField] Transform _spawnPoint_8;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public Vector3 GetSpawnPoint(ESpawnPoint spawnPoint)
    {
        switch (spawnPoint)
        {
            case ESpawnPoint._Player: return _spawnPoint_Player != null ? _spawnPoint_Player.position : Vector3.zero;
            case ESpawnPoint._1: return _spawnPoint_1 != null ? _spawnPoint_1.position : Vector3.zero;
            case ESpawnPoint._2: return _spawnPoint_2 != null ? _spawnPoint_2.position : Vector3.zero;
            case ESpawnPoint._3: return _spawnPoint_3 != null ? _spawnPoint_3.position : Vector3.zero;
            case ESpawnPoint._4: return _spawnPoint_4 != null ? _spawnPoint_4.position : Vector3.zero;
            case ESpawnPoint._5: return _spawnPoint_5 != null ? _spawnPoint_5.position : Vector3.zero;
            case ESpawnPoint._6: return _spawnPoint_6 != null ? _spawnPoint_6.position : Vector3.zero;
            case ESpawnPoint._7: return _spawnPoint_7 != null ? _spawnPoint_7.position : Vector3.zero;
            case ESpawnPoint._8:return _spawnPoint_8 != null ? _spawnPoint_8.position : Vector3.zero;
            default:return Vector3.zero;
        }
    }
}
