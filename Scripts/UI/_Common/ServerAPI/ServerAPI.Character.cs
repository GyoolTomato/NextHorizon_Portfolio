using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_CharacterAcquire(int characterKey, int stack,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_CharacterOperation("/api/character/acquire", characterKey, stack,
            json => ParseResponse(json, Parse_CharacterAcquire, onSuccess), onFailure);
    }

    public void Send_CharacterList(
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/character/list", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserCharacterRequest { uid = GameData.Instance.pPlayerInfo.pUid }),
            json => ParseResponse(json, Parse_CharacterList, onSuccess), onFailure);
    }

    public void Send_CharacterUpgrade(int characterKey,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/character/upgrade", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerCharacterRequest { uid = GameData.Instance.pPlayerInfo.pUid, characterKey = characterKey }),
            json => ParseResponse(json, Parse_CharacterUpgrade, onSuccess), onFailure);
    }

    public void Send_CharacterUpdate(int characterKey, int stack,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_CharacterOperation("/api/character/update", characterKey, stack,
            json => ParseResponse(json, Parse_CharacterUpdate, onSuccess), onFailure);
    }

    public void Send_CharacterLevelUp(int characterKey, EItemType[] eItemTypes, long[] counts,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCharacterLevelUpRequest request = new ServerCharacterLevelUpRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid, characterKey = characterKey, eItemTypes = eItemTypes, counts = counts,
        };

        //
        SendJson("/api/character/level-up", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_CharacterLevelUp, onSuccess), onFailure);
    }

    private void Send_CharacterOperation(string path, int characterKey, int stack,
        Action<string> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCharacterRequest request = new ServerCharacterRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid, characterKey = characterKey, stack = stack
        };

        //
        SendJson(path, UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request), onSuccess, onFailure);
    }

    public bool Parse_CharacterAcquire(string json) => Parse_CharacterOperation(json);
    public bool Parse_CharacterList(string json)
    {
        //
        ServerArrayResponse<ServerPlayerCharacterData> response =
            JsonUtility.FromJson<ServerArrayResponse<ServerPlayerCharacterData>>($"{{\"items\":{json}}}");

        if (response == null)
            return false;

        //
        ServerPlayerCharacterData[] characters = response.items ?? Array.Empty<ServerPlayerCharacterData>();

        // Post-process

        //
        var packet = new Observer.CharacterListReceivedEvent(characters);

        //
        Observer.ObserverTracker<Observer.CharacterListReceivedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_CharacterUpgrade(string json)
    {
        //
        ServerPlayerCharacterData character = JsonUtility.FromJson<ServerPlayerCharacterData>(json);

        if (character == null)
            return false;

        // Post-process

        //
        var packet = new Observer.CharacterUpgradedEvent(character);

        //
        Observer.ObserverTracker<Observer.CharacterUpgradedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_CharacterUpdate(string json) => Parse_CharacterOperation(json);

    public bool Parse_CharacterLevelUp(string json)
    {
        //
        ServerCharacterLevelUpResponse response = JsonUtility.FromJson<ServerCharacterLevelUpResponse>(json);

        if (response == null)
            return false;

        // Post-process
        var character = GameData.Instance.pDataCharacter.GetCharacter(response.characterKey);
        if (character == null)
            return false;

        character.pLevel = response.level;
        character.pExp = response.exp;

        if (response.items != null)
        {
            foreach (var item in response.items)
            {
                var tableInfo = _101_Items.GetItem(item.itemKey);
                if (tableInfo == null)
                    continue;

                GameData.Instance.pDataInventory.GetDataItem(tableInfo.type)?.SetItemCount(item.quantity);
            }
        }

        //
        var packet = new Observer.CharacterLevelUpEvent(response);

        //
        Observer.ObserverTracker<Observer.CharacterLevelUpEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_CharacterOperation(string json)
    {
        //
        ServerItemOperationResponse response = JsonUtility.FromJson<ServerItemOperationResponse>(json);

        //
        bool success = response != null && response.success;

        // Post-process

        //
        var packet = new Observer.CharacterOperationSucceededEvent(success);

        //
        Observer.ObserverTracker<Observer.CharacterOperationSucceededEvent>.Instance.Broadcast(packet);

        //
        return success;
    }
}
