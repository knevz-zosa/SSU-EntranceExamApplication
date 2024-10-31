namespace Common.Security;
public interface IPasswordHasher
{
    bool VerifyPassword(string passwordHash, string inputPassword);
    string Hash(string password);
}
