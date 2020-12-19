namespace NosCore.Shared.Authentication
{
    public interface IEncryption
    {
        string Encrypt(string password, string? salt);
        string Encrypt(string password);
    }
}