using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Toast : MonoBehaviour, IToast
{
    private readonly float _circleSpeed = 180f;

    public event Action OnClickConfirm;

    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _circle;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _contentText;
    [SerializeField] private Button _confirmButton;

    private float _circleAngle;

    private void Awake()
    {
        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(OnCLickConfirm);

        Hide();
    }

    private void Update()
    {
        if (_content.activeSelf & _circle.activeSelf)
        {
            _circleAngle += _circleSpeed * Time.deltaTime;
            _circle.transform.localEulerAngles = _circleAngle * Vector3.back;
        }
    }

    public void ShowLoading(string title)
    {
        _titleText.text = title;
        _circleAngle = 0;
        _confirmButton.gameObject.SetActive(false);
        _contentText.gameObject.SetActive(false);
        _circle.SetActive(true);
        _content.SetActive(true);
    }

    public void ShowInfo(string title, string text)
    {
        _titleText.text = title;
        _contentText.text = text;
        _confirmButton.gameObject.SetActive(true);
        _contentText.gameObject.SetActive(true);
        _circle.SetActive(false);
        _content.SetActive(true);
    }

    public void Hide()
    {
        _confirmButton.gameObject.SetActive(false);
        _contentText.gameObject.SetActive(false);
        _circle.SetActive(false);
        _content.SetActive(false);
    }

    private void OnCLickConfirm()
    {
        Hide();
        OnClickConfirm?.Invoke();
    }
}