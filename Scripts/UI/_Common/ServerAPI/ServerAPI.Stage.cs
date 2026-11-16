using System;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI
{
    public void Send_StageClear(int stageKey, int remainingTime,
        Action<bool> onSuccess, Action<ServerAPIError> onFailure)
    {
        //
        ServerStageClearRequest request = new ServerStageClearRequest
        {
            uid = GameData.Instance.pPlayerInfo.pUid,
            stageKey = stageKey,
            remainingTime = remainingTime,
        };

        //
        SendJson("/api/stage/clear", UnityWebRequest.kHttpVerbPOST,
            JsonUtility.ToJson(request),
            json => ParseResponse(json, Parse_StageClear, onSuccess), onFailure);
    }

    public bool Parse_StageClear(string json)
    {
        //
        if (string.IsNullOrWhiteSpace(json))
            return false;

        //
        ServerStageClearResponse response;
        try
        {
            response = JsonUtility.FromJson<ServerStageClearResponse>(json);
        }
        catch (ArgumentException)
        {
            return false;
        }

        if (response == null || response.starCount < 1 || response.starCount > 3)
            return false;

        //
        if (GameData.Instance.pDataStage.pDicStageState.ContainsKey(response.stageKey) == false)
            return false;

        //
        GameData.Instance.pDataStage.pDicStageState[response.stageKey].SetCondition(response.starCount);

        //
        foreach (var item in response.weapons)
        {
            //
            var tableInfo = _106_Weapons.GetItem(item.weaponKey);
            if (tableInfo == null)
            {
                continue;
            }

            //
            var weaponData = new DataWeapon(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);
            GameData.Instance.pDataInventory.AddWeapon(weaponData);
        }

        //
        foreach (var item in response.armors)
        {
            //
            var tableInfo = _105_Armors .GetItem(item.armorKey);
            if (tableInfo == null)
            {
                continue;
            }

            //
            var armorData = new DataArmor(item.id, item.level, item.exp, item.equipedCharacter, item.subStat0, item.subStat1, tableInfo);
            GameData.Instance.pDataInventory.AddArmor(armorData);
        }

        //
        foreach (var item in response.rewards)
        {
            //
            if (GameData.Instance.pDataInventory.pDicItems.ContainsKey(item.itemKey))
            {
                GameData.Instance.pDataInventory.pDicItems[item.itemKey].AddItemCount(item.quantity);
            }
        }

        //
        var packet = new Observer.StageClearEvent(response);

        //
        Observer.ObserverTracker<Observer.StageClearEvent>.Instance.Broadcast(packet);

        //
        return true;
    }
}
