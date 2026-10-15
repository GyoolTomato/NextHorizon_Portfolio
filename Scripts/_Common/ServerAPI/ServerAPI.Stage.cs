using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_StageClear(int stageKey, float clearTime,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerStageClearRequest request = new ServerStageClearRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            stageKey = stageKey,
            clearTime = clearTime,
        };

        //
        SendJson("/api/stage/clear", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_StageClear, onSuccess), onFailure);
    }

    public bool Parse_StageClear(string json)
    {
        //
        if (string.IsNullOrWhiteSpace(json))
            return false;

        //
        ServerStageClearResponse response;
        try
        {
            response = JsonUtility.FromJson<ServerStageClearResponse>(json);
        }
        catch (ArgumentException)
        {
            return false;
        }

        if (response == null || response.success == false)
            return false;

        // Post-process

        //
        var packet = new Observer.StageClearEvent(response);

        //
        Observer.ObserverTracker<Observer.StageClearEvent>.Instance.Broadcast(packet);

        //
        return true;
    }
}
