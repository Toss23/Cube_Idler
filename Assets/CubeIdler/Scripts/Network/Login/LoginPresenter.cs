public class LoginPresenter
{
    private ILoginView _loginView;
    private LoginForm _loginForm;

    public LoginPresenter(ILoginView loginView)
    {
        Network network = new Network();

        _loginView = loginView;
        _loginForm = new LoginForm(network);
    }

    public void Enable()
    {
        _loginView.OnClickLogin += (login, password) => _loginForm.Login(login, password);
        _loginView.OnApplicationFocused += (focus) => _loginForm.ChangeStatus(focus);
        _loginForm.OnLogged += OnLogged;
    }

    public void Disable()
    {
        _loginView.OnClickLogin -= (login, password) => _loginForm.Login(login, password);
        _loginView.OnApplicationFocused -= (focus) => _loginForm.ChangeStatus(focus);
        _loginForm.OnLogged -= OnLogged;
    }

    private void OnLogged(string login)
    {
        _loginView.Hide();
    } 
}