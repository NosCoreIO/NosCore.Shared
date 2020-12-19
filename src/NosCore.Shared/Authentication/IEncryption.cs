namespace NosCore.Shared.Authentication
{
    public interface IHasher
    {
        string Hash(string password, string? salt);
        string Hash(string password);
    }
}