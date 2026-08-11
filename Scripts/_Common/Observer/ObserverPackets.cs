using System;

// 기존 ServerAPI partial 클래스와의 호환성을 위해
// 서버 API JSON 패킷은 전역 네임스페이스에 둔다.
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

namespace Observer
{
    public readonly struct LoginSucceededEvent : IObserverEvent
    {
        public ServerUserData User { get; }

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

    public readonly struct ServerRequestFailedEvent : IObserverEvent
    {
        public ServerAPIError Error { get; }

        public ServerRequestFailedEvent(ServerAPIError error)
        {
            Error = error;
        }
    }
}
