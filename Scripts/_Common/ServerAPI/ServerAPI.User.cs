using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_GuestLogin(string localId,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerLoginRequest request = new ServerLoginRequest { localId = localId };

        //
        SendJson("/api/user/login", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_Login, onSuccess), onFailure);
    }

    public void Send_CreateGuest(string localId, string nickname,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCreateUserRequest request = new ServerCreateUserRequest
        {
            localId = localId, nickname = nickname
        };

        //
        SendJson("/api/user", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_CreateUser, onSuccess), onFailure);
    }

    public void Send_FirebaseLogin(string firebaseToken,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        var request = new ServerFirebaseRequest { firebaseToken = firebaseToken };
        SendJson("/api/auth/firebase", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_Login, onSuccess), onFailure);
    }

    public void Send_CreateFirebase(string firebaseToken, string nickname,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        var request = new ServerFirebaseRequest { firebaseToken = firebaseToken, nickname = nickname };
        SendJson("/api/auth/firebase/create", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_CreateUser, onSuccess), onFailure);
    }

    public void Send_LinkFirebase(string firebaseToken,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        var request = new ServerFirebaseRequest {
            firebaseToken = firebaseToken, uid = GameData.Instance.pPlayerInfo.pUid
        };
        SendJson("/api/auth/link", UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_LinkFirebase, onSuccess), onFailure);
    }

    public bool Parse_LinkFirebase(string json)
    {
        ServerUserData user = JsonUtility.FromJson<ServerUserData>(json);
        if (user?.playerInfo == null || string.IsNullOrWhiteSpace(user.playerInfo.uid))
            return false;

        //
        var packet = new Observer.FirebaseLinkedEvent(user);

        //
        Observer.ObserverTracker<Observer.FirebaseLinkedEvent>.Instance.Broadcast(packet);

        return true;
    }

    public void Send_ChangeNickname(string nickname,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerChangeNicknameRequest request = new ServerChangeNicknameRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid, nickname = nickname
        };

        //
        SendJson("/api/user/nickname", "PATCH", JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_ChangeNickname, onSuccess), onFailure);
    }

    public bool Parse_Login(string json)
    {
        //
        ServerLoginResponse response = JsonUtility.FromJson<ServerLoginResponse>(json);

        if (response == null)
            return false;

        // Post-process

        //
        var packet = new Observer.LoginResponseParsedEvent(response);

        //
        Observer.ObserverTracker<Observer.LoginResponseParsedEvent>.Instance.Broadcast(packet);

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
        var packet = new Observer.LoginSucceededEvent(user);

        //
        Observer.ObserverTracker<Observer.LoginSucceededEvent>.Instance.Broadcast(packet);

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
        var packet = new Observer.NicknameChangedEvent(user.playerInfo.uid, user.playerInfo.nickname);

        //
        Observer.ObserverTracker<Observer.NicknameChangedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }
}
