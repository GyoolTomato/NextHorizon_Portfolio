using System;

namespace Observer
{
    public readonly struct LoginResponseParsedEvent : IObserverEvent
    {
        public ServerLoginResponse Response { get; }

        public LoginResponseParsedEvent(ServerLoginResponse response)
        {
            Response = response;
        }
    }

    public readonly struct LoginSucceededEvent : IObserverEvent
    {
        public ServerUserData User { get; }
        public ServerPlayerItemData[] Items => User?.items ?? Array.Empty<ServerPlayerItemData>();
        public ServerPlayerCharacterData[] Characters => User?.characters ?? Array.Empty<ServerPlayerCharacterData>();
        public ServerPlayerArmorData[] Armors => User?.armors ?? Array.Empty<ServerPlayerArmorData>();
        public ServerPlayerWeaponData[] Weapons => User?.weapons ?? Array.Empty<ServerPlayerWeaponData>();

        public LoginSucceededEvent(ServerUserData user)
        {
            User = user;
        }
    }

    public readonly struct NewUserRequiredEvent : IObserverEvent
    {
        public string LocalId { get; }
        public string FirebaseUid { get; }

        public NewUserRequiredEvent(string localId, string firebaseUid)
        {
            LocalId = localId;
            FirebaseUid = firebaseUid;
        }
    }

    public readonly struct NicknameChangedEvent : IObserverEvent
    {
        public int UserId { get; }
        public string Nickname { get; }

        public NicknameChangedEvent(int userId, string nickname)
        {
            UserId = userId;
            Nickname = nickname;
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
        public ServerPlayerItemData[] Items { get; }

        public ItemListReceivedEvent(ServerPlayerItemData[] items)
        {
            Items = items;
        }
    }

    public readonly struct ArmorListReceivedEvent : IObserverEvent
    {
        public ServerPlayerArmorData[] Armors { get; }

        public ArmorListReceivedEvent(ServerPlayerArmorData[] armors)
        {
            Armors = armors;
        }
    }

    public readonly struct WeaponListReceivedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData[] Weapons { get; }

        public WeaponListReceivedEvent(ServerPlayerWeaponData[] weapons)
        {
            Weapons = weapons;
        }
    }

    public readonly struct ArmorEquippedEvent : IObserverEvent
    {
        public ServerPlayerArmorData Armor { get; }

        public ArmorEquippedEvent(ServerPlayerArmorData armor)
        {
            Armor = armor;
        }
    }

    public readonly struct WeaponEquippedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData Weapon { get; }

        public WeaponEquippedEvent(ServerPlayerWeaponData weapon)
        {
            Weapon = weapon;
        }
    }

    public readonly struct ArmorReleasedEvent : IObserverEvent
    {
        public ServerPlayerArmorData Armor { get; }

        public ArmorReleasedEvent(ServerPlayerArmorData armor)
        {
            Armor = armor;
        }
    }

    public readonly struct WeaponReleasedEvent : IObserverEvent
    {
        public ServerPlayerWeaponData Weapon { get; }

        public WeaponReleasedEvent(ServerPlayerWeaponData weapon)
        {
            Weapon = weapon;
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
        public ServerPlayerCharacterData[] Characters { get; }

        public CharacterListReceivedEvent(ServerPlayerCharacterData[] characters)
        {
            Characters = characters;
        }
    }

    public readonly struct CharacterUpgradedEvent : IObserverEvent
    {
        public ServerPlayerCharacterData Character { get; }

        public CharacterUpgradedEvent(ServerPlayerCharacterData character)
        {
            Character = character;
        }
    }

    public readonly struct CharacterLevelUpEvent : IObserverEvent
    {
        public ServerCharacterLevelUpResponse Character { get; }

        public CharacterLevelUpEvent(ServerCharacterLevelUpResponse character)
        {
            Character = character;
        }
    }

    public readonly struct VersionReceivedEvent : IObserverEvent
    {
        public ServerVersionResponse Version { get; }

        public VersionReceivedEvent(ServerVersionResponse version)
        {
            Version = version;
        }
    }
}
