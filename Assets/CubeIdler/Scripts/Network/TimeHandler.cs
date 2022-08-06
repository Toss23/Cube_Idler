using System;
using System.Reflection;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public static TimeHandler Instance;

    public event Action OnSynchronized;

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
            OnSynchronized?.Invoke();
        });
    }

    private void Update()
    {
        _milliseconds += Time.deltaTime * 1000;
    }
}