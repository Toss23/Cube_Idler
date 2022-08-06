using System;

public class LoginForm
{
    public event Action<string> OnLogged;

    private Network _network;
    private bool _logged;
    private string _login;

    public LoginForm(Network network)
    {
        _network = network;
    }

    public void Login(string login, string password)
    {
        string path = string.Format("/users/{0}/password", login);
        _network.Request<string>(path, (requestPassword) =>
        {
            if (requestPassword.Status == RequestStatus.Completed)
            {
                if (requestPassword.Value == password)
                {
                    _logged = true;
                    _login = login;
                    ChangeStatus(true);
                    OnLogged?.Invoke(login);
                }
            }
        });
    }

    public void ChangeStatus(bool online)
    {
        if (_logged)
        {
            string pathOnline = string.Format("/users/{0}/online", _login);
            _network.Post(pathOnline, online);
        }
    }
}