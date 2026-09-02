using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_Title_Login : Com_Base
{
    private const string GoogleWebClientId =
        "693264845451-4019fn8u077jb0nl7jrq0t69ju7cbmnj.apps.googleusercontent.com";

    //
    [SerializeField] GameObject _btnLogInGoogle;
    [SerializeField] GameObject _btnLogInGuest;
    [SerializeField] GameObject _btnLogOut;
    [SerializeField] TextMeshProUGUI _txtLogInType;
    [SerializeField] GameObject _btnMessageForPlayGame;
    [SerializeField] TextMeshProUGUI _txtMessage;

    //
    public enum EState
    {
        None,
        LogIn_Google,
        LogIn_Guest,
        Loading,
    }

    //
    EState               _state = EState.None;
    private FirebaseAuth _auth;
    private bool         _isInitialized = false;
    private GoogleSignInConfiguration _googleSignInConfiguration;

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        Debug.Log("Init start");

        _isInitialized = false;
        EnsureGoogleSignInConfiguration();

        _btnLogInGoogle.SetActive(false);
        _btnLogInGuest.SetActive(false);
        _btnLogOut.SetActive(false);
        _txtLogInType.gameObject.SetActive(false);
        _btnMessageForPlayGame.SetActive(false);
        _txtMessage.gameObject.SetActive(false);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result != DependencyStatus.Available)
            {
                Debug.LogError($"Firebase dependency error: {task.Result}");
                return;
            }

            FirebaseApp app = FirebaseApp.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;

            _isInitialized = true;

            SetState(GetCurrentLogInType());

            Debug.Log("Firebase / GoogleSignIn initialized");
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public EState GetCurrentLogInType()
    {
        if (_auth.CurrentUser == null)
        {
            return EState.None;
        }
        else if (_auth.CurrentUser.IsAnonymous)
        {
            return EState.LogIn_Guest;
        }
        else
        {
            return EState.LogIn_Google;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void SetState(EState state)
    {
        _state = state;

        RefreshState(_state);
    }

    /// <summary>
    /// 
    /// </summary>
    void RefreshState(EState state)
    {
        _btnLogInGoogle         .SetActive(state == EState.None);
        _btnLogInGuest          .SetActive(state == EState.None);
        _btnLogOut              .SetActive(state == EState.LogIn_Google || state == EState.LogIn_Guest);
        _txtLogInType.gameObject.SetActive(state != EState.None);
        _btnMessageForPlayGame  .SetActive(state != EState.None && state != EState.Loading);
        _txtMessage  .gameObject.SetActive(state != EState.None);

        if (_txtLogInType.gameObject.activeSelf)
        {
            _txtLogInType.text = _auth.CurrentUser.IsAnonymous ? "Guest Log In" : "Google Log In";
        }
        
        if (_txtMessage.gameObject.activeSelf)
        {
            _txtMessage.text = state != EState.Loading ? "Press Touch For Play Game" : "Loading...";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnGoogleLogin()
    {
#if UNITY_EDITOR
        Debug.LogWarning(
            "Google Sign-In is not supported in the Unity Editor. " +
            "Build and run the app on an Android or iOS device.");
        return;
#elif !UNITY_ANDROID && !UNITY_IOS
        Debug.LogWarning("Google Sign-In is only supported on Android and iOS.");
        return;
#else
        //
        if (_state != EState.None)
        {
            return;
        }

        //
        if (!_isInitialized)
        {
            Debug.LogError("Login system is not initialized yet.");
            return;
        }

        try
        {
            EnsureGoogleSignInConfiguration();
            GoogleSignIn.DefaultInstance.EnableDebugLogging(true);
            Debug.Log($"Google Sign-In start: package={Application.identifier}, webClientId={GoogleSignIn.Configuration.WebClientId}");
            GoogleSignIn.DefaultInstance.SignIn()
                .ContinueWithOnMainThread(OnGoogleAuthFinished);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
#endif
    }

    private void EnsureGoogleSignInConfiguration()
    {
        _googleSignInConfiguration ??= new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebClientId,
            RequestIdToken = true,
            RequestEmail = true
        };

        GoogleSignIn.Configuration = _googleSignInConfiguration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    private void OnGoogleAuthFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsCanceled)
        {
            Debug.LogError("Google Sign-In was canceled.");
            return;
        }

        if (task.IsFaulted)
        {
            System.Exception innerException = task.Exception?.GetBaseException();
            if (innerException is GoogleSignIn.SignInException signInException)
            {
                Debug.LogError(
                    $"Google Sign-In error: status={signInException.Status}, " +
                    $"message={signInException.Message}");
            }
            else
            {
                Debug.LogError($"Google Sign-In error: {task.Exception}");
            }
            return;
        }

        GoogleSignInUser googleUser = task.Result;

        if (googleUser == null || string.IsNullOrEmpty(googleUser.IdToken))
        {
            Debug.LogError("Google user or IdToken is null.");
            return;
        }

        Credential credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);

        _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
        {
            if (authTask.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }

            if (authTask.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + authTask.Exception);
                return;
            }

            FirebaseUser newUser = authTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            SetState(GetCurrentLogInType());
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnGuestLogin()
    {
        //
        if (_state != EState.None)
        {
            return;
        }

        //
        _auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(authTask =>
        {
            if (authTask.IsCanceled)
            {
                Debug.LogError("Anonymous Sign-In was canceled.");
                return;
            }
            if (authTask.IsFaulted)
            {
                Debug.LogError("Anonymous Sign-In encountered an error: " + authTask.Exception);
                return;
            }

            FirebaseUser newUser = authTask.Result.User;
            Debug.LogFormat("User signed in anonymously: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            SetState(GetCurrentLogInType());
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnLogOut()
    {
        //
        if (_state == EState.None || _state == EState.Loading)
        {
            return;
        }

        //
        _auth.SignOut();

        //
#if !UNITY_EDITOR
        if (GoogleSignIn.Configuration != null)
        {
            GoogleSignIn.DefaultInstance.SignOut();
        }
#endif

        //
        SetState(GetCurrentLogInType());
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBtnStartGame()
    {
        //
        if (_state == EState.None || _state == EState.Loading)
        {
            return;
        }

        //
        LogoScene.pStateLogIn.DoLogin();
    }
}
