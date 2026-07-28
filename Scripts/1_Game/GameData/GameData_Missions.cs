using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// 
/// </summary>
public class DataMission
{
    //
    public _104_Missions.Values pTableInfo { private set; get; } = null;
    public long                 pCount     { private set; get; } = 0;
    public bool                 pIsTake    { private set; get; } = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="isTake"></param>
    public DataMission(_104_Missions.Values tableInfo, long count, bool isTake)
    {
        pTableInfo = tableInfo;
        pCount     = count;
        pIsTake    = isTake;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddCount(long count)
    {
        pCount += count;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ApplyCount(long count)
    {
        pCount = count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsMissionCompleted()
    {
        return pTableInfo.count <= pCount;
    }
}

/// <summary>
/// 
/// </summary>
public class GameData_Missions
{
    //
    public List<DataMission>            pMissions    { get; set; } = new List<DataMission>();
    public Dictionary<int, DataMission> pDicMissions { get; set; } = new Dictionary<int, DataMission>();


    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        //
        pMissions.Clear();
        pDicMissions.Clear();

        //
        foreach (var item in _104_Missions.GetList())
        {
            var temp = new DataMission(item, 0, false);
            pMissions.Add(temp);
            pDicMissions.Add(item.key, temp);
        }
    }
}