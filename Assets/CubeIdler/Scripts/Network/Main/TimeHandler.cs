using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public static TimeHandler Instance;

    private bool _isSynchronized;
    private double _milliseconds;

    public bool IsSynchronized { get { return _isSynchronized; } }
    public double Milliseconds { get { return _milliseconds; } }

    private void Awake()
    {
        Instance = this;
        Application.runInBackground = true;
    }

    private void Start()
    {
        Network network = new Network();
        network.ServerTime((milliseconds, dateTime) => 
        {
            _milliseconds = milliseconds;
            _isSynchronized = true;
        });
    }

    private void Update()
    {
        _milliseconds += Time.deltaTime * 1000;
    }
}