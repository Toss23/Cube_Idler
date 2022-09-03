using System;

public interface IToast
{
    public event Action OnClickConfirm;

    public void ShowLoading(string title);
    public void ShowInfo(string title, string text);
    public void Hide();
}