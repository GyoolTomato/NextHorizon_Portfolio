using System.Collections.Generic;
using UnityEngine;

public class Manager_SelectTarget : Singleton<Manager_SelectTarget>
{
    GameData_InGame _dataInGame;

    /// <summary>
    /// 전투 중인 캐릭터 목록을 가져옵니다.
    /// </summary>
    public void Init()
    {
        _dataInGame = GameData.Instance.pDataInGame;
    }

    /// <summary>
    /// 가장 가까운 적을 반환합니다.
    /// </summary>
    public Character GetEnemyNearest(Character character)
    {
        List<Character> candidates = GetEnemyCandidates(character);
        if (candidates == null)
            return null;

        Character nearest = null;
        float nearestDistance = float.MaxValue;
        Vector3 origin = character.transform.position;

        foreach (Character candidate in candidates)
        {
            if (IsInvalidTarget(character, candidate))
                continue;

            float distance = (candidate.transform.position - origin).sqrMagnitude;
            if (distance >= nearestDistance)
                continue;

            nearestDistance = distance;
            nearest = candidate;
        }

        return nearest;
    }

    /// <summary>
    /// 가까운 순서대로 최대 count명의 적을 반환합니다.
    /// </summary>
    public List<Character> GetEnemiesNearest(Character character, int count)
    {
        var result = new List<Character>();
        if (count <= 0)
            return result;

        List<Character> candidates = GetEnemyCandidates(character);
        if (candidates == null)
            return result;

        var distances = new List<float>();
        Vector3 origin = character.transform.position;

        foreach (Character candidate in candidates)
        {
            if (IsInvalidTarget(character, candidate))
                continue;

            float distance = (candidate.transform.position - origin).sqrMagnitude;
            int insertIndex = distances.Count;

            for (int i = 0; i < distances.Count; i++)
            {
                if (distance < distances[i])
                {
                    insertIndex = i;
                    break;
                }
            }

            if (insertIndex >= count)
                continue;

            distances.Insert(insertIndex, distance);
            result.Insert(insertIndex, candidate);

            if (result.Count > count)
            {
                int lastIndex = result.Count - 1;
                result.RemoveAt(lastIndex);
                distances.RemoveAt(lastIndex);
            }
        }

        return result;
    }

    List<Character> GetEnemyCandidates(Character character)
    {
        if (character == null || character.pData == null)
            return null;

        _dataInGame ??= GameData.Instance.pDataInGame;
        if (_dataInGame == null)
            return null;

        switch (character.pData.pAllyType)
        {
            case EAllyType.Ally:
                return _dataInGame.pEnemies;

            case EAllyType.Enemy:
                return _dataInGame.pAllies;

            default:
                return null;
        }
    }

    bool IsInvalidTarget(Character character, Character target)
    {
        return target == null ||
               target == character ||
               target.pData == null ||
               target.gameObject.activeInHierarchy == false ||
               target.pComStats.IsDead();
    }
}
