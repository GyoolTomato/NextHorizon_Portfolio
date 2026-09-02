using System;

[Serializable]
public class ServerLoginRequest
{
    public string localId;
    public string firebaseUid;
}

[Serializable]
public class ServerCreateUserRequest : ServerLoginRequest
{
    public string nickname;
}

[Serializable]
public class ServerChangeNicknameRequest
{
    public string localId;
    public string nickname;
}

[Serializable]
public class ServerLoginResponse
{
    public bool isNew;
    public ServerUserData user;
}

[Serializable]
public class ServerUserData
{
    public int id;
    public string localId;
    public string firebaseUid;
    public string nickname;
    public int level;
    public ServerPlayerItemData[] items;
    public ServerPlayerCharacterData[] characters;
    public ServerPlayerArmorData[] armors;
    public ServerPlayerWeaponData[] weapons;
}

[Serializable]
public class ServerErrorResponse
{
    public string error;
}

[Serializable]
public class ServerAPIError
{
    public long statusCode;
    public string message;

    public override string ToString()
    {
        return $"HTTP {statusCode}: {message}";
    }
}

[Serializable]
public class ServerItemRequest
{
    public int userId;
    public int itemKey;
    public int quantity;
}

[Serializable]
public class ServerUserItemRequest
{
    public int userId;
}

[Serializable]
public class ServerItemOperationResponse
{
    public bool success;
}

[Serializable]
public class ServerPlayerItemData
{
    public int userId;
    public int itemKey;
    public int quantity;
}

[Serializable]
public class ServerPlayerArmorData
{
    public int id;
    public int userId;
    public int armorKey;
    public int level;
    public int exp;
    public int equipedCharacter;
}

[Serializable]
public class ServerPlayerWeaponData
{
    public int id;
    public int userId;
    public int weaponKey;
    public int level;
    public int exp;
    public int equipedCharacter;
}

[Serializable]
public class ServerArmorEquipRequest
{
    public int userId;
    public int characterKey;
    public int id;
}

[Serializable]
public class ServerWeaponEquipRequest
{
    public int userId;
    public int characterKey;
    public int id;
}

[Serializable]
public class ServerInventoryReleaseRequest
{
    public int userId;
    public int id;
}

[Serializable]
public class ServerCharacterRequest
{
    public int userId;
    public int characterKey;
    public int stack;
}

[Serializable]
public class ServerCharacterLevelUpRequest
{
    public int userId;
    public int characterKey;
    public EItemType[] eItemTypes;
    public long[] counts;
}

[Serializable]
public class ServerCharacterLevelUpResponse
{
    public int characterKey;
    public int level;
    public long exp;
    public ServerPlayerItemData[] items;
}

[Serializable]
public class ServerUserCharacterRequest
{
    public int userId;
}

[Serializable]
public class ServerPlayerCharacterData
{
    public int userId;
    public int characterKey;
    public int stack;
    public long exp;
    public int level;
    public int grade;
    public int activeLv;
    public int charm;
    public int passiveLv0;
    public int passiveLv1;
    public int passiveLv2;
}

[Serializable]
public class ServerArrayResponse<T>
{
    public T[] items;
}

[Serializable]
public class ServerVersionResponse
{
    public string nowVersion;
    public string downloadUrl;
    public string createdAt;
}
