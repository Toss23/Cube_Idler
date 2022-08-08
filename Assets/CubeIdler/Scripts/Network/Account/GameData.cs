public class GameData : DatabaseComponent
{
    public string Gold { get { return GetPath("gold"); } }

    public GameData(string path) : base(path) { }
}