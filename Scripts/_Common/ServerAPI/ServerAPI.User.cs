using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_Login(
        string localId,
        string firebaseUid,
        Action<ServerLoginResponse> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        ServerLoginRequest request = new ServerLoginRequest
        {
            localId = localId,
            firebaseUid = firebaseUid
        };

        SendJson(
            "/api/user/login",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_Login(json)),
            onFailure);
    }

    public ServerLoginResponse Parse_Login(string json)
    {
        return JsonUtility.FromJson<ServerLoginResponse>(json);
    }

    public void Send_CreateUser(
        string localId,
        string firebaseUid,
        string nickname,
        Action<ServerUserData> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        ServerCreateUserRequest request = new ServerCreateUserRequest
        {
            localId = localId,
            firebaseUid = firebaseUid,
            nickname = nickname
        };

        SendJson(
            "/api/user",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_CreateUser(json)),
            onFailure);
    }

    public ServerUserData Parse_CreateUser(string json)
    {
        return JsonUtility.FromJson<ServerUserData>(json);
    }

    public void Send_ChangeNickname(
        string localId,
        string nickname,
        Action<ServerUserData> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        ServerChangeNicknameRequest request = new ServerChangeNicknameRequest
        {
            localId = localId,
            nickname = nickname
        };

        SendJson(
            "/api/user/nickname",
            "PATCH",
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_ChangeNickname(json)),
            onFailure);
    }

    public ServerUserData Parse_ChangeNickname(string json)
    {
        return JsonUtility.FromJson<ServerUserData>(json);
    }
}
