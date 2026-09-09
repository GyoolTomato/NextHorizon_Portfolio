using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_ItemAcquire(int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/acquire", itemKey, quantity,
            json => ParseResponse(json, Parse_ItemAcquire, onSuccess), onFailure);
    }

    public void Send_ItemConsume(int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/consume", itemKey, quantity,
            json => ParseResponse(json, Parse_ItemConsume, onSuccess), onFailure);
    }

    public void Send_ItemUpdate(int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/update", itemKey, quantity,
            json => ParseResponse(json, Parse_ItemUpdate, onSuccess), onFailure);
    }

    public void Send_ItemList(
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/item/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { uid = GameData.Instance.pPlayerInfo.pUid }),
            json => ParseResponse(json, Parse_ItemList, onSuccess),
            onFailure);
    }

    public void Send_ArmorList(
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/armor/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { uid = GameData.Instance.pPlayerInfo.pUid }),
            json => ParseResponse(json, Parse_ArmorList, onSuccess),
            onFailure);
    }

    public void Send_WeaponList(
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/weapon/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { uid = GameData.Instance.pPlayerInfo.pUid }),
            json => ParseResponse(json, Parse_WeaponList, onSuccess),
            onFailure);
    }

    public void Send_ArmorEquip(int characterKey, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerArmorEquipRequest request = new ServerArmorEquipRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            characterKey = characterKey,
            id = id,
        };

        //
        SendJson("/api/armor/equip", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_ArmorEquip, onSuccess), onFailure);
    }

    public void Send_WeaponEquip(int characterKey, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerWeaponEquipRequest request = new ServerWeaponEquipRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            characterKey = characterKey,
            id = id,
        };

        //
        SendJson("/api/weapon/equip", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_WeaponEquip, onSuccess), onFailure);
    }

    public void Send_ArmorRelease(int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/armor/release", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerInventoryReleaseRequest { uid = GameData.Instance.pPlayerInfo.pUid, id = id }),
            json => ParseResponse(json, Parse_ArmorRelease, onSuccess), onFailure);
    }

    public void Send_WeaponRelease(int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/weapon/release", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerInventoryReleaseRequest { uid = GameData.Instance.pPlayerInfo.pUid, id = id }),
            json => ParseResponse(json, Parse_WeaponRelease, onSuccess), onFailure);
    }

    private void Send_ItemOperation(string path, int itemKey, int quantity,
        Action<string> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerItemRequest request = new ServerItemRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            itemKey = itemKey,
            quantity = quantity
        };

        //
        SendJson(path, UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(request), onSuccess, onFailure);
    }

    public bool Parse_ItemAcquire(string json) => Parse_ItemOperation(json);
    public bool Parse_ItemConsume(string json) => Parse_ItemOperation(json);
    public bool Parse_ItemUpdate(string json) => Parse_ItemOperation(json);

    public bool Parse_ItemOperation(string json)
    {
        //
        ServerItemOperationResponse response =
            JsonUtility.FromJson<ServerItemOperationResponse>(json);

        //
        bool success = response != null && response.success;

        //
        var packet = new Observer.ItemOperationSucceededEvent(success);

        //
        Observer.ObserverTracker<Observer.ItemOperationSucceededEvent>.Instance.Broadcast(packet);

        //
        return success;
    }

    public bool Parse_ItemList(string json)
    {
        //
        ServerArrayResponse<ServerPlayerItemData> response =
            JsonUtility.FromJson<ServerArrayResponse<ServerPlayerItemData>>($"{{\"items\":{json}}}");

        if (response == null)
            return false;

        //
        ServerPlayerItemData[] items = response.items ?? Array.Empty<ServerPlayerItemData>();

        // Post-process

        //
        var packet = new Observer.ItemListReceivedEvent(items);

        //
        Observer.ObserverTracker<Observer.ItemListReceivedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_ArmorList(string json)
    {
        //
        ServerArrayResponse<ServerPlayerArmorData> response =
            JsonUtility.FromJson<ServerArrayResponse<ServerPlayerArmorData>>($"{{\"items\":{json}}}");

        if (response == null)
            return false;

        //
        ServerPlayerArmorData[] armors = response.items ?? Array.Empty<ServerPlayerArmorData>();

        // Post-process

        //
        var packet = new Observer.ArmorListReceivedEvent(armors);

        //
        Observer.ObserverTracker<Observer.ArmorListReceivedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_WeaponList(string json)
    {
        //
        ServerArrayResponse<ServerPlayerWeaponData> response =
            JsonUtility.FromJson<ServerArrayResponse<ServerPlayerWeaponData>>($"{{\"items\":{json}}}");

        if (response == null)
            return false;

        //
        ServerPlayerWeaponData[] weapons = response.items ?? Array.Empty<ServerPlayerWeaponData>();

        // Post-process

        //
        var packet = new Observer.WeaponListReceivedEvent(weapons);

        //
        Observer.ObserverTracker<Observer.WeaponListReceivedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_ArmorEquip(string json)
    {
        //
        ServerPlayerArmorData armor = JsonUtility.FromJson<ServerPlayerArmorData>(json);

        if (armor == null)
            return false;

        // Post-process

        //
        var packet = new Observer.ArmorEquippedEvent(armor);

        //
        Observer.ObserverTracker<Observer.ArmorEquippedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_WeaponEquip(string json)
    {
        //
        ServerPlayerWeaponData weapon = JsonUtility.FromJson<ServerPlayerWeaponData>(json);

        if (weapon == null)
            return false;

        // Post-process

        //
        var packet = new Observer.WeaponEquippedEvent(weapon);

        //
        Observer.ObserverTracker<Observer.WeaponEquippedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_ArmorRelease(string json)
    {
        //
        ServerPlayerArmorData armor = JsonUtility.FromJson<ServerPlayerArmorData>(json);

        if (armor == null)
            return false;

        // Post-process

        //
        var packet = new Observer.ArmorReleasedEvent(armor);

        //
        Observer.ObserverTracker<Observer.ArmorReleasedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }

    public bool Parse_WeaponRelease(string json)
    {
        //
        ServerPlayerWeaponData weapon = JsonUtility.FromJson<ServerPlayerWeaponData>(json);

        if (weapon == null)
            return false;

        // Post-process

        //
        var packet = new Observer.WeaponReleasedEvent(weapon);

        //
        Observer.ObserverTracker<Observer.WeaponReleasedEvent>.Instance.Broadcast(packet);

        //
        return true;
    }
}
