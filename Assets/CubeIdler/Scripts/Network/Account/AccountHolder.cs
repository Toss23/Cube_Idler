using UnityEngine;

public class AccountHolder : MonoBehaviour, IAccountHolder
{
    private Account _account;

    public Account Current { get { return _account; } }

    public void Auth(string login)
    {
        _account = new Account(login);

        Network network = new Network();
        network.Request<string>(_account.Name, (data) =>
        {
            if (data.Status == RequestStatus.Completed)
                Debug.Log("Auth: " + data.Value);
        });
    }
}