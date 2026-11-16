using System;
using System.Collections.Generic;
using System.Text;

public class GameData_PlayerInfo
{
    //
    public string pPortrait { private set; get; }
    public string pUid { private set; get; }
    public string pUserNickName { private set; get; }
    public string pCreatedAt { private set; get; }
    public int pLevel { private set; get; }
    public long pExp { private set; get; }
    public string pIntroduction { private set; get; }    
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userData"></param>
    public void Init(ServerPlayerInfoData playerInfo)
    {
        pPortrait = playerInfo.portrait;
        pUid = playerInfo.uid;
        pUserNickName = playerInfo.nickname;
        pLevel = playerInfo.level;
        pExp = playerInfo.exp;
        pCreatedAt = DateTime.Parse(playerInfo.createdAt).ToString("yyyy-MM-dd");
        pIntroduction = playerInfo.introduction;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="experienceData"></param>
    public void SetPlayerExperience(ServerPlayerExperienceData experienceData)
    {
        if (experienceData == null)
            return;

        pLevel = experienceData.level;
        pExp = experienceData.exp;
    }

    public void SetIntroduction(ServerPlayerInfoData playerInfo)
    {
        pIntroduction = playerInfo.introduction;
    }
}
