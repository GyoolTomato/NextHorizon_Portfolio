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
            json => onSuccess?.Invoke(Parse_Version(json)), onFailure);
    }

    public bool Parse_Version(string json)
    {
        //
        ServerVersionResponse response = JsonUtility.FromJson<ServerVersionResponse>(json);

        if (response == null)
            return false;

        // Post-process

        //
        Observer.ObserverTracker<Observer.VersionReceivedEvent>.Instance.Broadcast(new Observer.VersionReceivedEvent(response));

        //
        return true;
    }
}
