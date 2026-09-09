using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class DataMission
{
    //
    public _104_Missions.Values pTableInfo { private set; get; } = null;
    public long                 pProgress  { private set; get; } = 0;
    public bool                 pIsClaimed { private set; get; } = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="isTake"></param>
    public DataMission(_104_Missions.Values tableInfo, long progress, bool isClaimed)
    {
        pTableInfo = tableInfo;
        pProgress  = progress;
        pIsClaimed = isClaimed;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddProgress(long count)
    {
        if (IsMissionCompleted())
        {
            return;
        }

        pProgress += count;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetProgress(long count)
    {
        pProgress = count;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetClaimed(bool isClaimed)
    {
        pIsClaimed = isClaimed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsMissionCompleted()
    {
        return pTableInfo.count <= pProgress;
    }
}

/// <summary>
/// 
/// </summary>
public class GameData_Missions
{
    //
    public List<DataMission>            pMissions    { private set; get; } = new List<DataMission>();
    public Dictionary<int, DataMission> pDicMissions { private set; get; } = new Dictionary<int, DataMission>();
    public Dictionary<EMissionType, List<DataMission>> pDicMissions_Type { private set; get; } = new Dictionary<EMissionType, List<DataMission>>();
    public Dictionary<EMissionCycleType, List<DataMission>> pDicMissions_CycleType { private set; get; } = new Dictionary<EMissionCycleType, List<DataMission>>();


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerMissionData[] missions)
    {
        //
        pMissions.Clear();
        pDicMissions.Clear();
        pDicMissions_Type.Clear();

        //
        foreach (var item in _104_Missions.GetList())
        {
            //
            var temp = new DataMission(item, 0, false);

            //
            pMissions.Add(temp);

            //
            if (pDicMissions.ContainsKey(item.key) == false)
            {
                pDicMissions.Add(item.key, temp);
            }

            //
            if (pDicMissions_Type.ContainsKey(item.type) == false)
                pDicMissions_Type.Add(item.type, new List<DataMission>());

            pDicMissions_Type[item.type].Add(temp);

            //
            if (pDicMissions_CycleType.ContainsKey(item.cycleType) == false)
                pDicMissions_CycleType.Add(item.cycleType, new List<DataMission>());

            pDicMissions_CycleType[item.cycleType].Add(temp);
        }

        //
        foreach (var item in missions)
        {
            if (pDicMissions.ContainsKey(item.missionKey))
            {
                var data = pDicMissions[item.missionKey];
                data.SetProgress(item.progress);
                data.SetClaimed(item.isClaimed);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddProgress(EMissionType missionType, long progress)
    {
        //
        if (pDicMissions_Type.ContainsKey(missionType) == false)
        {
            Debug.LogError("pDicMissions_Type.ContainsKey(missionType) == false : " + missionType);
            return;
        }

        //
        foreach (var item in pDicMissions_Type[missionType])
        {
            item.AddProgress(progress);
        }
    }
}