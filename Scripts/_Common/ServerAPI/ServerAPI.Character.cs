using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_CharacterAcquire(int userId, int characterKey, int stack,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_CharacterOperation("/api/character/acquire", userId, characterKey, stack,
            json => onSuccess?.Invoke(Parse_CharacterAcquire(json)), onFailure);
    }

    public void Send_CharacterList(int userId,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/character/list", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserCharacterRequest { userId = userId }),
            json => onSuccess?.Invoke(Parse_CharacterList(json)), onFailure);
    }

    public void Send_CharacterUpgrade(int userId, int characterKey,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/character/upgrade", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerCharacterRequest { userId = userId, characterKey = characterKey }),
            json => onSuccess?.Invoke(Parse_CharacterUpgrade(json)), onFailure);
    }

    public void Send_CharacterUpdate(int userId, int characterKey, int stack,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_CharacterOperation("/api/character/update", userId, characterKey, stack,
            json => onSuccess?.Invoke(Parse_CharacterUpdate(json)), onFailure);
    }

    public void Send_CharacterLevelUp(int userId, int characterKey, EItemType[] eItemTypes, long[] counts,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCharacterLevelUpRequest request = new ServerCharacterLevelUpRequest
        {
            userId = userId, characterKey = characterKey, eItemTypes = eItemTypes, counts = counts,
        };

        //
        SendJson("/api/character/level-up", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_CharacterLevelUp(json)), onFailure);
    }

    private void Send_CharacterOperation(string path, int userId, int characterKey, int stack,
        Action<string> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerCharacterRequest request = new ServerCharacterRequest
        {
            userId = userId, characterKey = characterKey, stack = stack
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
        Observer.ObserverTracker<Observer.CharacterListReceivedEvent>.Instance.Broadcast(new Observer.CharacterListReceivedEvent(characters));

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
        Observer.ObserverTracker<Observer.CharacterUpgradedEvent>.Instance.Broadcast(new Observer.CharacterUpgradedEvent(character));

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
        Observer.ObserverTracker<Observer.CharacterLevelUpEvent>.Instance.Broadcast(new Observer.CharacterLevelUpEvent(response));

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
        Observer.ObserverTracker<Observer.CharacterOperationSucceededEvent>.Instance.Broadcast(new Observer.CharacterOperationSucceededEvent(success));

        //
        return success;
    }
}
