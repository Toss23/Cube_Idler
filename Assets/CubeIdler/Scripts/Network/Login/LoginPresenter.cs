using UnityEngine;

public class LoginPresenter : MonoBehaviour
{
    [SerializeField] private Toast toast;
    [SerializeField] private AccountHolder accountHandler;
    [SerializeField] private LoginView loginView;

    private IToast _toast;
    private IAccountHolder _accountHandler;
    private ILoginView _loginView;
    private LoginForm _loginForm;

    private void Awake()
    {
        _toast = toast;
        _accountHandler = accountHandler;
        _loginView = loginView;
        _loginForm = new LoginForm();

        _loginView.Show();

        Enable();
    }

    public void Enable()
    {
        _loginView.OnClickLogin += OnClickLogin;
        _loginForm.OnAuthSuccess += OnAuthSuccess;
        _loginForm.OnAuthDenied += OnAuthDenied;
        _loginForm.OnIncorrectParams += OnIncorrectParams;
    }

    public void Disable()
    {
        _loginView.OnClickLogin -= OnClickLogin;
        _loginForm.OnAuthSuccess -= OnAuthSuccess;
        _loginForm.OnAuthDenied -= OnAuthDenied;
        _loginForm.OnIncorrectParams -= OnIncorrectParams;
    }

    private void OnClickLogin(string login, string password)
    {
        _toast.ShowLoading("Request Account permission");
        _loginForm.Auth(login, password);
    }

    private void OnIncorrectParams()
    {
        _toast.Hide();
        _toast.ShowInfo("Error", "Login and password minimum 3 sign");
    }

    private void OnAuthSuccess(string login)
    {
        _toast.Hide();
        _loginView.Hide();
        _accountHandler.Auth(login);
    }

    private void OnAuthDenied(RequestStatus status)
    {
        _toast.Hide();
        _toast.ShowInfo("Error", "Permission denied");
    }
}