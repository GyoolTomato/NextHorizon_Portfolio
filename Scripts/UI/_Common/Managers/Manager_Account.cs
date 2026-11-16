using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using UnityEngine;

public class Manager_Account : Singleton<Manager_Account>
{
    //
    const string GoogleWebClientId =
        "693264845451-4019fn8u077jb0nl7jrq0t69ju7cbmnj.apps.googleusercontent.com";
    const string LoginTypePreferenceKey = "NextHorizon.LoginType";
    const string LegacyGuestLoginPreferenceKey = "NextHorizon.GuestLogin";

    //
    public enum ELoginType
    {
        None,
        Guest,
        Google,
    }

    //
    FirebaseAuth _auth;
    GoogleSignInConfiguration _googleSignInConfiguration;

    //
    public bool pIsInitialized { private set; get; }
    public ELoginType pLoginType { private set; get; }


    /// <summary>
    /// 
    /// </summary>
    public Manager_Account()
    {
        pLoginType = LoadLoginType();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onComplete"></param>
    public void Init(Action<bool> onComplete = null)
    {
        //
        if (pIsInitialized)
        {
            onComplete?.Invoke(true);
            return;
        }

        //
        EnsureGoogleSignInConfiguration();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted || task.Result != DependencyStatus.Available)
            {
                string error = string.Empty;
                if (task.IsCanceled)
                {
                    error = "canceled";
                }
                else if (task.IsFaulted)
                {
                    error = task.Exception?.GetBaseException().Message;
                }
                else
                {
                    error = task.Result.ToString();
                }

                Debug.LogError($"Firebase dependency error: {error}");
                onComplete?.Invoke(false);
                return;
            }

            //
            _auth = FirebaseAuth.DefaultInstance;
            if (_auth.CurrentUser != null && _auth.CurrentUser.IsAnonymous)
            {
                _auth.SignOut();
            }

            if (pLoginType != ELoginType.Guest)
            {
                ELoginType loginType = _auth.CurrentUser != null ? ELoginType.Google : ELoginType.None;
                SetLoginType(loginType);
            }

            //
            pIsInitialized = true;
            onComplete?.Invoke(true);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void LogInGuest()
    {
        //
        SetLoginType(ELoginType.Guest);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onComplete"></param>
    public void LogInGoogle(Action<bool> onComplete = null)
    {
#if UNITY_EDITOR
        Debug.LogWarning(
            "Google Sign-In is not supported in the Unity Editor. " +
            "Build and run the app on an Android or iOS device.");
        onComplete?.Invoke(false);
#elif !UNITY_ANDROID && !UNITY_IOS
        Debug.LogWarning("Google Sign-In is only supported on Android and iOS.");
        onComplete?.Invoke(false);
#else
        //
        if (!pIsInitialized)
        {
            Debug.LogError("Login system is not initialized yet.");
            onComplete?.Invoke(false);
            return;
        }

        //
        try
        {
            EnsureGoogleSignInConfiguration();
            GoogleSignIn.DefaultInstance.EnableDebugLogging(true);
            GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(
                task => OnGoogleAuthFinished(task, onComplete));
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            onComplete?.Invoke(false);
        }
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onSuccess"></param>
    /// <param name="onFailure"></param>
    public void GetFirebaseToken(Action<string> onSuccess, Action<Exception> onFailure = null)
    {
        //
        FirebaseUser firebaseUser = _auth?.CurrentUser;
        if (firebaseUser == null)
        {
            onFailure?.Invoke(new InvalidOperationException("Firebase current user is not ready."));
            return;
        }

        //
        firebaseUser.TokenAsync(false).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                onFailure?.Invoke(task.Exception?.GetBaseException()
                    ?? new InvalidOperationException("Firebase token request was canceled."));
                return;
            }

            onSuccess?.Invoke(task.Result);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isClearPlayerData"></param>
    public void LogOut(bool isClearPlayerData = true)
    {
        //
        ServerAPI.Instance.Clear();

        //
        _auth?.SignOut();

#if !UNITY_EDITOR
        if (GoogleSignIn.Configuration != null)
        {
            GoogleSignIn.DefaultInstance.SignOut();
        }
#endif

        //
        if (isClearPlayerData)
        {
            Manager_Observer.Instance.ClearAll();
            GameData.Instance.Init();
        }

        //
        SetLoginType(ELoginType.None);
    }

    /// <summary>
    /// 
    /// </summary>
    void EnsureGoogleSignInConfiguration()
    {
        //
        if (_googleSignInConfiguration == null)
        {
            _googleSignInConfiguration = new GoogleSignInConfiguration
            {
                WebClientId = GoogleWebClientId,
                RequestIdToken = true,
                RequestEmail = true,
            };
        }

        //
        GoogleSignIn.Configuration = _googleSignInConfiguration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onComplete"></param>
    void OnGoogleAuthFinished(Task<GoogleSignInUser> task, Action<bool> onComplete)
    {
        //
        if (task.IsCanceled)
        {
            Debug.LogError("Google Sign-In was canceled.");
            onComplete?.Invoke(false);
            return;
        }

        if (task.IsFaulted)
        {
            Exception exception = task.Exception?.GetBaseException();
            if (exception is GoogleSignIn.SignInException signInException)
            {
                Debug.LogError(
                    $"Google Sign-In error: status={signInException.Status}, " +
                    $"message={signInException.Message}");
            }
            else
            {
                Debug.LogError($"Google Sign-In error: {exception}");
            }
            onComplete?.Invoke(false);
            return;
        }

        //
        GoogleSignInUser googleUser = task.Result;
        if (googleUser == null || string.IsNullOrEmpty(googleUser.IdToken))
        {
            Debug.LogError("Google user or IdToken is null.");
            onComplete?.Invoke(false);
            return;
        }

        //
        Credential credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);
        _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
        {
            if (authTask.IsCanceled || authTask.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync failed: " + authTask.Exception);
                onComplete?.Invoke(false);
                return;
            }

            SetLoginType(ELoginType.Google);
            onComplete?.Invoke(true);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ELoginType LoadLoginType()
    {
        //
        if (PlayerPrefs.HasKey(LoginTypePreferenceKey))
        {
            int value = PlayerPrefs.GetInt(LoginTypePreferenceKey, (int)ELoginType.None);
            if (Enum.IsDefined(typeof(ELoginType), value))
            {
                return (ELoginType)value;
            }
        }

        //
        if (PlayerPrefs.GetInt(LegacyGuestLoginPreferenceKey, 0) == 1)
        {
            PlayerPrefs.DeleteKey(LegacyGuestLoginPreferenceKey);
            PlayerPrefs.SetInt(LoginTypePreferenceKey, (int)ELoginType.Guest);
            PlayerPrefs.Save();
            return ELoginType.Guest;
        }

        return ELoginType.None;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginType"></param>
    void SetLoginType(ELoginType loginType)
    {
        //
        pLoginType = loginType;
        PlayerPrefs.SetInt(LoginTypePreferenceKey, (int)loginType);
        PlayerPrefs.DeleteKey(LegacyGuestLoginPreferenceKey);
        PlayerPrefs.Save();
    }
}
