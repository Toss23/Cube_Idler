using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _targetFps = 60;
    [SerializeField] private bool _VSync = false;
    [SerializeField] private bool _displayFps = false;

    private TMP_Text _text;

    private float _timer;
    private int _frames;
    private float _fps;

    private void Awake()
    {
        QualitySettings.vSyncCount = _VSync ? 1 : 0;
        Application.targetFrameRate = _targetFps;

        if (_displayFps)
        {
            _text = GetComponent<TMP_Text>();
        }
        _frames = 0;
        _timer = 1f;
    }

    void Update()
    {
        _frames++;

        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _fps = _frames - 1;
            _frames = 0;
            _timer = 1f;
        }

        _text.text = Mathf.Round(_fps).ToString();
    }
}
