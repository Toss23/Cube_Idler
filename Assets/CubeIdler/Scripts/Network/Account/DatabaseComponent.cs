public class DatabaseComponent
{
    private string _path;

    public DatabaseComponent(string path)
    {
        _path = path;
    }

    protected string GetPath(string name)
    {
        return string.Format("{0}/{1}", _path, name);
    }
}