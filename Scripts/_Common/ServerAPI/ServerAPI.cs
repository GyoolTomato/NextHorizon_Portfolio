using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public partial class ServerAPI : MonoBehaviour
{
    private static ServerAPI _instance;

    public static ServerAPI Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject(nameof(ServerAPI));
                _instance = gameObject.AddComponent<ServerAPI>();
                DontDestroyOnLoad(gameObject);
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
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
        string url = $"{ProgramSettings.Instance.pServerAddress}{path}";
        using UnityWebRequest request = new UnityWebRequest(url, method);
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        string response = request.downloadHandler?.text ?? string.Empty;
        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure?.Invoke(new ServerAPIError
            {
                statusCode = request.responseCode,
                message = Parse_Error(response, request.error)
            });
            yield break;
        }

        onSuccess?.Invoke(response);
    }

    private string Parse_Error(string json, string fallback)
    {
        if (!string.IsNullOrWhiteSpace(json))
        {
            ServerErrorResponse response = JsonUtility.FromJson<ServerErrorResponse>(json);
            if (response != null && !string.IsNullOrWhiteSpace(response.error))
            {
                return response.error;
            }
        }

        return fallback;
    }
}
