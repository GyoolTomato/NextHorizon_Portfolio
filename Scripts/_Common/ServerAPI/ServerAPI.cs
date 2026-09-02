using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI : MonoBehaviour
{
    //
    private static ServerAPI _instance;

    //
    public static ServerAPI Instance
    {
        get
        {
            if (_instance == null)
            {
                //
                GameObject gameObject = new GameObject(nameof(ServerAPI));
                _instance = gameObject.AddComponent<ServerAPI>();
                DontDestroyOnLoad(gameObject);
            }

            //
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);

            //
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void SendJson(
        string path,
        string method,
        string json,
        Action<string> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        StartCoroutine(CoSendJson(path, method, json, onSuccess, onFailure));
    }

    private IEnumerator CoSendJson(
        string path,
        string method,
        string json,
        Action<string> onSuccess,
        Action<ServerAPIError> onFailure)
    {
        //
        ServerAPIError lastError = new ServerAPIError
        {
            message = "서버 주소가 설정되지 않았습니다."
        };

        //
        string[] serverAddresses = ProgramSettings.Instance.pServerAddresses;
        if (serverAddresses == null || serverAddresses.Length == 0)
        {
            onFailure?.Invoke(lastError);

            //
            yield break;
        }

        for (int i = 0; i < serverAddresses.Length; i++)
        {
            //
            string serverAddress = ProgramSettings.Instance.GetServerAddress(i);
            if (string.IsNullOrWhiteSpace(serverAddress))
            {
                continue;
            }

            //
            string url = $"{serverAddress}{path}";

            //
            using UnityWebRequest request = new UnityWebRequest(url, method);
            request.timeout = 5;
            if (!string.IsNullOrEmpty(json))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            }
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            //
            yield return request.SendWebRequest();

            //
            string response = request.downloadHandler?.text ?? string.Empty;
            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response);

                //
                yield break;
            }

            lastError.statusCode = request.responseCode;
            lastError.message = request.error;
            Parse_Error(response, lastError);

            Debug.LogError($"서버 요청 실패: {method} {url}, 상태 코드: {request.responseCode}, 오류: {lastError.message}, 응답: {response}");

            // 연결 자체에 실패한 경우에만 다음 서버를 시도한다.
            // 4xx/5xx 응답은 서버에 도달한 것이므로 재시도하지 않는다.
            if (request.result != UnityWebRequest.Result.ConnectionError)
            {
                break;
            }

            Debug.LogWarning($"서버 접속 실패 ({i + 1}/{serverAddresses.Length}): {url}");
        }

        onFailure?.Invoke(lastError);
    }

    private bool Parse_Error(string json, ServerAPIError error)
    {
        if (error == null || string.IsNullOrWhiteSpace(json))
            return false;

        //
        ServerErrorResponse response = JsonUtility.FromJson<ServerErrorResponse>(json);
        if (response == null || string.IsNullOrWhiteSpace(response.error))
            return false;

        //
        error.message = response.error;
        return true;
    }
}
