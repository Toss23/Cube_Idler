using System;

public interface ILoginView
{
    public event Action<string, string> OnClickLogin;

    public void Show();
    public void Hide();
}