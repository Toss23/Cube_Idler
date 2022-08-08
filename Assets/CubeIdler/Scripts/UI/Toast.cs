using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour, IToast
{
    private readonly float _circleSpeed = 180f;

    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _circle;
    [SerializeField] private TMP_Text _title;

    private float _circleAngle;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (_content.activeSelf)
        {
            _circleAngle += _circleSpeed * Time.deltaTime;
            _circle.transform.localEulerAngles = _circleAngle * Vector3.back;
        }
    }

    public void Show(string text)
    {
        _title.text = text;
        _circleAngle = 0;
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }
}