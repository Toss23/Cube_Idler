public class Account : DatabaseComponent
{
    public string Name { get { return GetPath("name"); } }
    public string Online { get { return GetPath("online"); } }
    public string Password { get { return GetPath("password"); } }
    public GameData GameData;

    public string Login { get; private set; }

    public Account(string login) : base(string.Format("users/{0}", login))
    {
        GameData = new GameData(GetPath("gameData"));
        Login = login;
    }
}