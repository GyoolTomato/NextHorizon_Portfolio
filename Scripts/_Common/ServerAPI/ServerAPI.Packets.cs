using System;

[Serializable]
public class ServerLoginRequest
{
    public string localId;
}

[Serializable]
public class ServerCreateUserRequest : ServerLoginRequest
{
    public string nickname;
}

[Serializable]
public class ServerFirebaseRequest
{
    public string firebaseToken;
    public string nickname;
    public string uid;
}

[Serializable]
public class ServerChangeNicknameRequest
{
    public string uid;
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
    public ServerPlayerInfoData playerInfo;
    public ServerPlayerItemData[] items;
    public ServerPlayerCharacterData[] characters;
    public ServerPlayerArmorData[] armors;
    public ServerPlayerWeaponData[] weapons;
    public ServerMissionData[] missions;
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
    public string uid;
    public int itemKey;
    public int quantity;
}

[Serializable]
public class ServerUserItemRequest
{
    public string uid;
}

[Serializable]
public class ServerItemOperationResponse
{
    public bool success;
    public ServerPlayerExperienceData playerExperience;
}

[Serializable]
public class ServerPlayerExperienceData
{
    public string uid;
    public int level;
    public long exp;
    public string createdAt;
    public long grantedExp;
    public long appliedExp;
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
    public string uid;
    public int characterKey;
    public int id;
}

[Serializable]
public class ServerWeaponEquipRequest
{
    public string uid;
    public int characterKey;
    public int id;
}

[Serializable]
public class ServerInventoryReleaseRequest
{
    public string uid;
    public int id;
}

[Serializable]
public class ServerCharacterRequest
{
    public string uid;
    public int characterKey;
    public int stack;
}

[Serializable]
public class ServerCharacterLevelUpRequest
{
    public string uid;
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
    public string uid;
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

[Serializable]
public class ServerPlayerInfoRequest
{
    public string uid;
    public string introduction;
    public string portrait;
}

[Serializable]
public class ServerPlayerInfoData
{
    public string uid;
    public string nickname;
    public int level;
    public long exp;
    public string portrait;
    public string introduction;
    public string createdAt;
}

[Serializable]
public class ServerMissionRequest
{
    public string uid;
    public int missionKey;
}

[Serializable]
public class ServerMissionData
{
    public int missionKey;
    public int progress;
    public bool isClaimed;
}

[Serializable]
public class ServerMissionListResponse
{
    public ServerMissionData[] missions;
}

[Serializable]
public class ServerMissionRewardData
{
    public int itemKey;
    public long quantity;
}

[Serializable]
public class ServerMissionClaimResponse : ServerMissionListResponse
{
    public bool success;
    public int missionKey;
    public long exp;
    public ServerMissionRewardData[] rewards;
    public ServerPlayerItemData[] items;
    public ServerPlayerExperienceData playerExperience;
}
