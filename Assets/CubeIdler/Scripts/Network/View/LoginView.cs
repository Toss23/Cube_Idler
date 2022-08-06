using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginView : MonoBehaviour, ILoginView
{
    public event Action<string, string> OnClickLogin;
    public event Action<bool> OnApplicationFocused;

    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _loginButton;

    private LoginPresenter _loginPresenter;

    private string _login { get { return _loginField.text; } }
    private string _password { get { return _passwordField.text; } }

    private void Awake()
    {
        _loginButton.onClick.RemoveAllListeners();
        _loginButton.onClick.AddListener(() => OnClickLogin?.Invoke(_login, _password));

        _loginPresenter = new LoginPresenter(this);
        _loginPresenter.Enable();
    }

    private void OnApplicationFocus(bool focus)
    {
        OnApplicationFocused?.Invoke(focus);
    }

    public void Show()
    {
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }
}