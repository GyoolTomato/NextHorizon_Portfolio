public enum ELanguage
{
    None,
    Korean,
    English,
    Japanese,
    End
}

public enum ESceneType
{
    None,
    Logo,
    Game,
    Play,
    End
}

public enum ELogoState
{
    None,
    Logo,
    Download,
    LogIn,
    End
}

public enum EGameState
{
    None,
    Main,
    Stage,
    End
}

public enum EPlayState
{
    None,
    Ready,
    Pause,
    Playing,
    Finish,
    End
}

public enum EPanelType
{
    None,
    Board_0,
    Title,
    Main,
    Characters,
    Gacha,
    Inventory,
    Shop,
    Mail,
    LobbyCharacter,
    Missions,
    StageSelect,
    Formation,
    Notice,
    PlayerInfo,
    CharacterInfo,
    CharacterSelect,
    WearEquipment,
    LevelUp,
    BattleController,
    BattleResult,
    Settings,
    Board_1,
    RewardBox,
    PopUpInfo,
    MessageBox,
    Flash,
    Board_2,
    TouchLock,
    End
}

public enum EItemType
{
    None,
    Crystal,
    Gold,
    PlayerExp,
    ExpCard,
    ReinforcementCard,
    End
}

public enum ECharacterAnim
{
    None,
    Idle,
    Walking,
    Running,
    Jumping,
    Attack,
    Die,
    Ceremony,
    End
}

public enum ECharacterState
{
    None,
    Idle,
    Walking,
    Running,
    Jumping,
    Attack,
    Die,
    Ceremony,
    Skill,
    End
}

public enum EEquipmentType
{
    None,
    Weapon,
    Hat,
    Clothes,
    Gloves,
    Shoes,
    End
}

public enum ECharacterImageType
{
    None,
    FullBody,
    Icon,
    Portrait,
    ActiveSkill,
    PassiveSkill1,
    PassiveSkill2,
    End
}

public enum ECharacterStats
{
    None,
    HP,
    HP_Level,
    ATK,
    ATK_Level,
    AtkCoolTime,
    AtkDelay,
    AtkDistance,
    DEF,
    DEF_Level,
    Avoid,
    Avoid_Level,
    Focus,
    Focus_level,
    AtkSpd,
    AtkSpd_level,
    Speed,
    Crirate,
    Crirate_level,
    Cridmg,
    Cridmg_level,
    ActiveSkill,
    PassiveSkill,
    End
}

public enum ESkillType
{
    None,
    ActiveSkill,
    PassiveSkill,
    End
}

public enum EGrade
{
    None,
    Normal,
    Rare,
    Elite,
    Epic,
    Legend,
    End
}

public enum EMissionType
{
    None,
    LogIn,
    Kill,
    GetIt_ExpCard,
    TimeOver,
    WorkCount,
    End
}

public enum EMissionCycleType
{
    None,
    Daily,
    Weekly,
    End
}

public enum EItemValueType
{
    None,
    Name,
    Grade,
    Level,
    End
}

public enum EStage
{
    None,
    _1,
    _2,
    _3,
    End
}

public enum EEquipStatType
{
    None,
    Main,
    Sub_0,
    Sub_1,
    End
}

public enum EAllyType
{
    None,
    Ally,
    Enemy,
    Neutrality,
    End
}

public enum ESpawnPoint
{
    None,
    _None,
    _Player,
    _1,
    _2,
    _3,
    _4,
    _5,
    _6,
    _7,
    _8,
    End
}

public enum ESpawnType
{
    None,
    Timer,
    End
}
