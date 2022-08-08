public interface IAccountHolder
{
    public Account Current { get; }

    public void Auth(string login);
}