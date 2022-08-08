using System;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class Network
{
    private DatabaseReference _reference;

    public Network()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Request<T>(string path, Action<NetData<T>> onRequested)
    {
        path = path.ToLower();
        NetData<T> requestData = new NetData<T>();

        _reference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                requestData.Status = RequestStatus.Denied;
            }
            else if (task.IsCompleted)
            {
                if (task.Result.Exists)
                {
                    Type resultType = task.Result.Value.GetType();
                    Type requestType = typeof(T);

                    if (Equals(resultType, requestType) == false)
                    {
                        requestData.Status = RequestStatus.DifferentTypes;
                    }
                    else
                    {
                        requestData.Status = RequestStatus.Completed;
                        requestData.Value = (T)task.Result.Value;
                    }
                }
                else
                {
                    requestData.Status = RequestStatus.NotExists;
                }
            }

            onRequested.Invoke(requestData);
        });
    }

    public void Post<T>(string path, T value, Action<NetData<T>> onPosted)
    {
        path = path.ToLower();
        NetData<T> postData = new NetData<T>();
        postData.Value = value;

        _reference.Child(path).SetValueAsync(value).ContinueWithOnMainThread(task => 
        {
            if (task.IsFaulted)
            {
                postData.Status = RequestStatus.Denied;
            }
            else if (task.IsCompleted)
            {
                postData.Status = RequestStatus.Completed;
            }

            onPosted.Invoke(postData);
        });
    }

    public void Post<T>(string path, T value)
    {
        path = path.ToLower();
        _reference.Child(path).SetValueAsync(value);
    }

    /// <summary>
    /// Server time (UTC) in milliseconds on client connected
    /// </summary>
    public void ServerTime(Action<double, DateTime> onLoaded)
    {
        _reference.Child("/.info/serverTimeOffset").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            double deltaMilliseconds = double.Parse(task.Result.GetRawJsonValue());
            double milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds() + deltaMilliseconds;
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(milliseconds);

            onLoaded.Invoke(milliseconds, dateTime);
        });
    }
}

public enum RequestStatus
{
    Completed, Denied, NotExists, DifferentTypes
}

public struct NetData<T>
{
    public RequestStatus Status;
    public T Value;
}