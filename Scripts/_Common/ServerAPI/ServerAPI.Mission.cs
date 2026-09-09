using System;
using UnityEngine;
using UnityEngine.Networking;

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

        foreach (var item in response.items)
        {
            var dataInventory = GameData.Instance.pDataInventory;
            if (dataInventory.pDicItems.ContainsKey(item.itemKey))
            {
                dataInventory.pDicItems[item.itemKey].AddItemCount(item.quantity);
            }
        }

        //
        var packet = new Observer.MissionClaimEvent(response);

        //
        Observer.ObserverTracker<Observer.MissionClaimEvent>.Instance.Broadcast(packet);

        return true;
    }
}
