using UnityEngine;

public class UserData
{
    public string uid;
    public string nickname;
    public int level;
    public int exp;
    public int gold;
}

public class TestLoginFlow : MonoBehaviour
{
    public void OnLoginSuccess(string uid)
    {
        UserData dummyData = new UserData
        {
            uid = uid,
            nickname = "TestUser",
            level = 1,
            exp = 20,
            gold = 1000
        };

        ApplyUserData(dummyData);
    }

    void ApplyUserData(UserData data)
    {
        Debug.Log($"UID: {data.uid}");
        Debug.Log($"닉네임: {data.nickname}");
        Debug.Log($"레벨: {data.level}");
        Debug.Log($"경험치: {data.exp}");
        Debug.Log($"골드: {data.gold}");
    }
}