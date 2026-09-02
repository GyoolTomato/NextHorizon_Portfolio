using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_Login(string localId, string firebaseUid,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerLoginRequest request = new ServerLoginRequest { localId = localId, firebaseUid = firebaseUid };

        //
        SendJson("/api/user/login", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_Login(json)), onFailure);
    }

    public void Send_CreateUser(string localId, string firebaseUid, string nickname,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCreateUserRequest request = new ServerCreateUserRequest
        {
            localId = localId, firebaseUid = firebaseUid, nickname = nickname
        };

        //
        SendJson("/api/user", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_CreateUser(json)), onFailure);
    }

    public void Send_ChangeNickname(string localId, string nickname,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerChangeNicknameRequest request = new ServerChangeNicknameRequest
        {
            localId = localId, nickname = nickname
        };

        //
        SendJson("/api/user/nickname", "PATCH", JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_ChangeNickname(json)), onFailure);
    }

    public bool Parse_Login(string json)
    {
        //
        ServerLoginResponse response = JsonUtility.FromJson<ServerLoginResponse>(json);

        if (response == null)
            return false;

        // Post-process

        //
        Observer.ObserverTracker<Observer.LoginResponseParsedEvent>.Instance.Broadcast(new Observer.LoginResponseParsedEvent(response));

        //
        return true;
    }

    public bool Parse_CreateUser(string json)
    {
        //
        ServerUserData user = JsonUtility.FromJson<ServerUserData>(json);

        if (user == null)
            return false;

        // Post-process

        //
        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Broadcast(new Observer.LoginSucceededEvent(user));

        //
        return true;
    }

    public bool Parse_ChangeNickname(string json)
    {
        //
        ServerUserData user = JsonUtility.FromJson<ServerUserData>(json);

        if (user == null)
            return false;

        // Post-process

        //
        Observer.ObserverTracker<Observer.NicknameChangedEvent>.Instance.Broadcast(new Observer.NicknameChangedEvent(user.id, user.nickname));

        //
        return true;
    }
}
