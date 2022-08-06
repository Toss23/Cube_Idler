using System;
using UnityEngine;

public interface ILoginView
{
    public event Action<string, string> OnClickLogin;
    public event Action<bool> OnApplicationFocused;

    public void Show();
    public void Hide();
}