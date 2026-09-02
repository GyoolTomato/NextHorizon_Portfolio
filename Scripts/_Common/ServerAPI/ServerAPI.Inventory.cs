using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_ItemAcquire(int userId, int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/acquire", userId, itemKey, quantity,
            json => onSuccess?.Invoke(Parse_ItemAcquire(json)), onFailure);
    }

    public void Send_ItemConsume(int userId, int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/consume", userId, itemKey, quantity,
            json => onSuccess?.Invoke(Parse_ItemConsume(json)), onFailure);
    }

    public void Send_ItemUpdate(int userId, int itemKey, int quantity,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        Send_ItemOperation("/api/item/update", userId, itemKey, quantity,
            json => onSuccess?.Invoke(Parse_ItemUpdate(json)), onFailure);
    }

    public void Send_ItemList(int userId,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/item/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { userId = userId }),
            json => onSuccess?.Invoke(Parse_ItemList(json)),
            onFailure);
    }

    public void Send_ArmorList(int userId,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/armor/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { userId = userId }),
            json => onSuccess?.Invoke(Parse_ArmorList(json)),
            onFailure);
    }

    public void Send_WeaponList(int userId,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson(
            "/api/weapon/list",
            UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerUserItemRequest { userId = userId }),
            json => onSuccess?.Invoke(Parse_WeaponList(json)),
            onFailure);
    }

    public void Send_ArmorEquip(int userId, int characterKey, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerArmorEquipRequest request = new ServerArmorEquipRequest
        {
            userId = userId,
            characterKey = characterKey,
            id = id,
        };

        //
        SendJson("/api/armor/equip", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_ArmorEquip(json)), onFailure);
    }

    public void Send_WeaponEquip(int userId, int characterKey, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerWeaponEquipRequest request = new ServerWeaponEquipRequest
        {
            userId = userId,
            characterKey = characterKey,
            id = id,
        };

        //
        SendJson("/api/weapon/equip", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => onSuccess?.Invoke(Parse_WeaponEquip(json)), onFailure);
    }

    public void Send_ArmorRelease(int userId, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/armor/release", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerInventoryReleaseRequest { userId = userId, id = id }),
            json => onSuccess?.Invoke(Parse_ArmorRelease(json)), onFailure);
    }

    public void Send_WeaponRelease(int userId, int id,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        SendJson("/api/weapon/release", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(new ServerInventoryReleaseRequest { userId = userId, id = id }),
            json => onSuccess?.Invoke(Parse_WeaponRelease(json)), onFailure);
    }

    private void Send_ItemOperation(string path, int userId, int itemKey, int quantity,
        Action<string> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerItemRequest request = new ServerItemRequest
        {
            userId = userId,
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

        // Post-process

        //
        Observer.ObserverTracker<Observer.ItemOperationSucceededEvent>.Instance.Broadcast(
            new Observer.ItemOperationSucceededEvent(success));

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
        Observer.ObserverTracker<Observer.ItemListReceivedEvent>.Instance.Broadcast(
            new Observer.ItemListReceivedEvent(items));

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
        Observer.ObserverTracker<Observer.ArmorListReceivedEvent>.Instance.Broadcast(
            new Observer.ArmorListReceivedEvent(armors));

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
        Observer.ObserverTracker<Observer.WeaponListReceivedEvent>.Instance.Broadcast(
            new Observer.WeaponListReceivedEvent(weapons));

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
        Observer.ObserverTracker<Observer.ArmorEquippedEvent>.Instance.Broadcast(new Observer.ArmorEquippedEvent(armor));

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
        Observer.ObserverTracker<Observer.WeaponEquippedEvent>.Instance.Broadcast(new Observer.WeaponEquippedEvent(weapon));

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
        Observer.ObserverTracker<Observer.ArmorReleasedEvent>.Instance.Broadcast(new Observer.ArmorReleasedEvent(armor));

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
        Observer.ObserverTracker<Observer.WeaponReleasedEvent>.Instance.Broadcast(new Observer.WeaponReleasedEvent(weapon));

        //
        return true;
    }
}
