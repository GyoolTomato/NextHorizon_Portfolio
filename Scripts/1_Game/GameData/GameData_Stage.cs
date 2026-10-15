using System;
using System.Collections.Generic;
using System.Text;

//
public class DataStage
{
    //
    public bool pIsCond_1 { private set; get; }
    public bool pIsCond_2 { private set; get; }
    public bool pIsCond_3 { private set; get; }
    public _700_StageList.Values pTableInfo { private set; get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo"></param>
    /// <param name="isCond_1"></param>
    /// <param name="isCond_2"></param>
    /// <param name="isCond_3"></param>
    public DataStage(_700_StageList.Values tableInfo, bool isCond_1, bool isCond_2, bool isCond_3)
    {
        pIsCond_1 = isCond_1;
        pIsCond_2 = isCond_2;
        pIsCond_3 = isCond_3;
        pTableInfo = tableInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isCond_1"></param>
    /// <param name="isCond_2"></param>
    /// <param name="isCond_3"></param>
    public void SetCondition(bool isCond_1, bool isCond_2, bool isCond_3)
    {
        pIsCond_1 = isCond_1;
        pIsCond_2 = isCond_2;
        pIsCond_3 = isCond_3;
    }
}

//
public class GameData_Stage
{
    //
    public List<DataStage> pStageState { private set; get; }
    public Dictionary<int, DataStage> pDicStageState { private set; get; }
    public Dictionary<EStage, List<DataStage>> pDicStageState_Stage { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public void Init(ServerStageClearData[] datas)
    {
        //
        pStageState ??= new List<DataStage>();
        pStageState.Clear();
        pDicStageState ??= new Dictionary<int, DataStage>();
        pDicStageState.Clear();
        pDicStageState_Stage ??= new Dictionary<EStage, List<DataStage>>();
        foreach (var item in pDicStageState_Stage)
        {
            item.Value.Clear();
        }
        pDicStageState_Stage.Clear();

        //
        foreach (var tableInfo in _700_StageList.GetList())
        {
            //
            var temp = new DataStage(tableInfo, false, false, false);
            
            //
            pStageState.Add(temp);

            //
            if (pDicStageState.ContainsKey(tableInfo.key) == false)
                pDicStageState.Add(tableInfo.key, null);

            pDicStageState[tableInfo.key] = temp;

            //
            if (pDicStageState_Stage.ContainsKey(tableInfo.stage) == false)
                pDicStageState_Stage.Add(tableInfo.stage, new List<DataStage>());

            pDicStageState_Stage[tableInfo.stage].Add(temp);
        }

        //
        if (datas == null)
            return;

        foreach (var data in datas)
        {
            if (pDicStageState.ContainsKey(data.stageKey))
            {
                pDicStageState[data.stageKey].SetCondition(data.isCond1, data.isCond2, data.isCond3);
            }
        }
    }
}
