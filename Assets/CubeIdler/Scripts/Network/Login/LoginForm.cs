using System;

public class LoginForm
{
    public event Action<string> OnAuthSuccess;
    public event Action<RequestStatus> OnAuthDenied;

    private Network _network;

    public LoginForm()
    {
        _network = new Network();
    }

    public void Auth(string login, string password)
    {
        Account account = new Account(login);

        _network.Request<string>(account.Password, (requestPassword) =>
        {
            if (requestPassword.Status == RequestStatus.Completed)
            {
                if (requestPassword.Value == password)
                    OnAuthSuccess?.Invoke(login);
                else
                    OnAuthDenied?.Invoke(requestPassword.Status);
            }
            else
            {
                OnAuthDenied?.Invoke(requestPassword.Status);
            }
        });
    }
}