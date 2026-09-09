using System;

namespace Observer
{
    public readonly struct LoginResponseParsedEvent : IObserverEvent
    {
        public ServerLoginResponse Data { get; }

        public LoginResponseParsedEvent(ServerLoginResponse data)
        {
            Data = data;
        }
    }

    public readonly struct LoginSucceededEvent : IObserverEvent
    {
        public ServerUserData User { get; }
        public ServerPlayerItemData[] Items => User?.items ?? Array.Empty<ServerPlayerItemData>();
        public ServerPlayerCharacterData[] Characters => User?.characters ?? Array.Empty<ServerPlayerCharacterData>();
        public ServerPlayerArmorData[] Armors => User?.armors ?? Array.Empty<ServerPlayerArmorData>();
        public ServerPlayerWeaponData[] Weapons => User?.weapons ?? Array.Empty<ServerPlayerWeaponData>();
        public ServerMissionData[] Missions => User?.missions ?? Array.Empty<ServerMissionData>();

        public LoginSucceededEvent(ServerUserData user)
        {
            User = user;
        }
    }

    public readonly struct NewUserRequiredEvent : IObserverEvent
    {
        public string LocalId { get; }

        public NewUserRequiredEvent(string localId)
        {
            LocalId = localId;
        }
    }

    public readonly struct NicknameChangedEvent : IObserverEvent
    {
        public string Uid { get; }
        public string Nickname { get; }

        public NicknameChangedEvent(string uid, string nickname)
        {
            Uid = uid;
            Nickname = nickname;
        }
    }

    public readonly struct FirebaseLinkedEvent : IObserverEvent
    {
        public ServerUserData Data { get; }

        public FirebaseLinkedEvent(ServerUserData data)
        {
            Data = data;
        }
    }

    public readonly struct PlayerInfoReceivedEvent : IObserverEvent
    {
        public ServerPlayerInfoData Data { get; }

        public PlayerInfoReceivedEvent(ServerPlayerInfoData data)
        {
            Data = data;
        }
    }

    public readonly struct IntroductionChangedEvent : IObserverEvent
    {
        public ServerPlayerInfoData Data { get; }

        public IntroductionChangedEvent(ServerPlayerInfoData data)
        {
            Data = data;
        }
    }

    public readonly struct PortraitChangedEvent : IObserverEvent
    {
        public ServerPlayerInfoData Data { get; }

        public PortraitChangedEvent(ServerPlayerInfoData data)
        {
            Data = data;
        }
    }

    public readonly struct ServerErrorParsedEvent : IObserverEvent
    {
        public ServerAPIError Data { get; }

        public ServerErrorParsedEvent(ServerAPIError data)
        {
            Data = data;
        }
    }

    public readonly struct ItemOperationSucceededEvent : IObserverEvent
    {
        public bool Success { get; }

        public ItemOperationSucceededEvent(bool success)
        {
            Success = success;
        }
    }

    public readonly struct ItemListReceivedEvent : IObserverEvent
    {
        public ServerPlayerItemData[] Data { get; }

        public ItemListReceivedEvent(ServerPlayerItemData[] data)
        {
            Data = data;
        }
    }

    public readonly struct ArmorListReceivedEvent : IObserverEvent
    {
        public ServerPlayerArmorData[] Data { get; }

        public ArmorListReceivedEvent(ServerPlayerArmorData[] data)
        {
            Data = data;
        }
    }

    public readonly struct WeaponListReceivedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData[] Data { get; }

        public WeaponListReceivedEvent(ServerPlayerWeaponData[] data)
        {
            Data = data;
        }
    }

    public readonly struct ArmorEquippedEvent : IObserverEvent
    {
        public ServerPlayerArmorData Data { get; }

        public ArmorEquippedEvent(ServerPlayerArmorData data)
        {
            Data = data;
        }
    }

    public readonly struct WeaponEquippedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData Data { get; }

        public WeaponEquippedEvent(ServerPlayerWeaponData data)
        {
            Data = data;
        }
    }

    public readonly struct ArmorReleasedEvent : IObserverEvent
    {
        public ServerPlayerArmorData Data { get; }

        public ArmorReleasedEvent(ServerPlayerArmorData data)
        {
            Data = data;
        }
    }

    public readonly struct WeaponReleasedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData Data { get; }

        public WeaponReleasedEvent(ServerPlayerWeaponData data)
        {
            Data = data;
        }
    }

    public readonly struct CharacterOperationSucceededEvent : IObserverEvent
    {
        public bool Success { get; }

        public CharacterOperationSucceededEvent(bool success)
        {
            Success = success;
        }
    }

    public readonly struct CharacterListReceivedEvent : IObserverEvent
    {
        public ServerPlayerCharacterData[] Data { get; }

        public CharacterListReceivedEvent(ServerPlayerCharacterData[] data)
        {
            Data = data;
        }
    }

    public readonly struct CharacterUpgradedEvent : IObserverEvent
    {
        public ServerPlayerCharacterData Data { get; }

        public CharacterUpgradedEvent(ServerPlayerCharacterData data)
        {
            Data = data;
        }
    }

    public readonly struct CharacterLevelUpEvent : IObserverEvent
    {
        public ServerCharacterLevelUpResponse Data { get; }

        public CharacterLevelUpEvent(ServerCharacterLevelUpResponse data)
        {
            Data = data;
        }
    }

    public readonly struct VersionReceivedEvent : IObserverEvent
    {
        public ServerVersionResponse Data { get; }

        public VersionReceivedEvent(ServerVersionResponse data)
        {
            Data = data;
        }
    }

    public readonly struct MissionListEvent : IObserverEvent
    {
        public ServerMissionListResponse Data { get; }

        public MissionListEvent(ServerMissionListResponse data)
        {
            Data = data;
        }
    }

    public readonly struct MissionClaimEvent:IObserverEvent
    {
        public ServerMissionClaimResponse Data { get; }

        public MissionClaimEvent(ServerMissionClaimResponse data)
        {
            Data = data;
        }
    }
}
