using System;
using UnityEngine;

public partial class ServerAPI
{
    public void Send_PlayerInfo(Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        var request = new ServerPlayerInfoRequest { uid = GameData.Instance.pPlayerInfo.pUid };
        SendJson("/api/player-info", UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_PlayerInfo, onSuccess), onFailure);
    }

    public void Send_ChangeIntroduction(string introduction, Action<bool> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        var request = new ServerPlayerInfoRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            introduction = introduction,
        };
        SendJson("/api/player-info/introduction", "PATCH", JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_ChangeIntroduction, onSuccess), onFailure);
    }

    public void Send_ChangePortrait(string portrait, Action<bool> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        var request = new ServerPlayerInfoRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            portrait = portrait,
        };
        SendJson("/api/player-info/portrait", "PATCH", JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_ChangePortrait, onSuccess), onFailure);
    }

    public bool Parse_ChangeIntroduction(string json)
    {
        if (TryParsePlayerInfo(json, out var response) == false)
            return false;

        //
        var packet = new Observer.IntroductionChangedEvent(response);

        GameData.Instance.pPlayerInfo.Init(packet.Data);

        //
        Observer.ObserverTracker<Observer.IntroductionChangedEvent>.Instance.Broadcast(packet);

        return true;
    }

    public bool Parse_ChangePortrait(string json)
    {
        if (TryParsePlayerInfo(json, out var response) == false)
            return false;

        //
        var packet = new Observer.PortraitChangedEvent(response);

        GameData.Instance.pPlayerInfo.Init(packet.Data);

        //
        Observer.ObserverTracker<Observer.PortraitChangedEvent>.Instance.Broadcast(packet);

        return true;
    }

    public bool Parse_PlayerInfo(string json)
    {
        if (TryParsePlayerInfo(json, out var response) == false)
            return false;

        //
        var packet = new Observer.PlayerInfoReceivedEvent(response);

        GameData.Instance.pPlayerInfo.Init(packet.Data);

        //
        Observer.ObserverTracker<Observer.PlayerInfoReceivedEvent>.Instance.Broadcast(packet);

        return true;
    }

    private bool TryParsePlayerInfo(string json, out ServerPlayerInfoData response)
    {
        response = null;
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try { response = JsonUtility.FromJson<ServerPlayerInfoData>(json); }
        catch (ArgumentException) { return false; }

        return response != null && string.IsNullOrWhiteSpace(response.uid) == false;
    }
}
