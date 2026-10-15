using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public partial class ServerAPI
{
    public void Send_MissionList(Action<bool> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        SendJson("/api/mission/list", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerMissionRequest { uid = GameData.Instance.pPlayerInfo.pUid }),
            json => ParseResponse(json, Parse_MissionList, onSuccess), onFailure);
    }

    public void Send_MissionClaim(int missionKey, Action<bool> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        SendJson("/api/mission/claim", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerMissionRequest { uid = GameData.Instance.pPlayerInfo.pUid, missionKey = missionKey }),
            json => ParseResponse(json, Parse_MissionClaim, onSuccess), onFailure);
    }

    public bool Parse_MissionList(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return false;

        ServerMissionListResponse response;
        try { response = JsonUtility.FromJson<ServerMissionListResponse>(json); }
        catch (ArgumentException) { return false; }

        if (response == null || response.missions == null)
            return false;

        //
        var packet = new Observer.MissionListEvent(response);

        //
        Observer.ObserverTracker<Observer.MissionListEvent>.Instance.Broadcast(packet);

        return true;
    }

    public bool Parse_MissionClaim(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return false;

        ServerMissionClaimResponse response;
        try { response = JsonUtility.FromJson<ServerMissionClaimResponse>(json); }
        catch (ArgumentException) { return false; }

        if (response == null || response.success == false || response.missions == null)
            return false;

        if (GameData.Instance.pDataMissions.pDicMissions.ContainsKey(response.missionKey) == false)
            return false;

        GameData.Instance.pDataMissions.pDicMissions[response.missionKey].SetClaimed(true);

        //
        var dataInventory = GameData.Instance.pDataInventory;        
        foreach (var item in response.items)
        {            
            //
            if (dataInventory.pDicItems.ContainsKey(item.itemKey))
            {
                dataInventory.pDicItems[item.itemKey].SetItemCount(item.quantity);
            }
        }
        
        //
        var dataItems = new List<DataItem>();
        for (int i = 0; i < response.rewards.Length; i++)
        {
            var tableInfo = _101_Items.GetItem(response.rewards[i].itemKey);
            if (tableInfo != null)
                dataItems.Add(new DataItem(response.rewards[i].quantity, tableInfo));            
        }

        var panel = Manager_UI.Instance.ShowPanel(EPanelType.RewardBox) as Panel_RewardBox;
        panel.Init(dataItems.ToArray(), null, null);

        //
        var packet = new Observer.MissionClaimEvent(response);

        //
        Observer.ObserverTracker<Observer.MissionClaimEvent>.Instance.Broadcast(packet);

        return true;
    }
}
