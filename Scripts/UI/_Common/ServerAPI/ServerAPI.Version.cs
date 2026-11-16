using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_Version(
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/version", UnityWebRequest.kHttpVerbGET, string.Empty,
            json => ParseResponse(json, Parse_Version, onSuccess), onFailure);
    }

    public bool Parse_Version(string json)
    {
        //
        ServerVersionResponse response = JsonUtility.FromJson<ServerVersionResponse>(json);

        if (response == null)
            return false;

        // Post-process

        //
        var packet = new Observer.VersionReceivedEvent(response);

        //
        Observer.ObserverTracker<Observer.VersionReceivedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }
}
