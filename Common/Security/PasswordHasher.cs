using System.Security.Cryptography;
namespace Common.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8; // 16 bytes
    private const int KeySize = 256 / 8; // 32 bytes
    private const int Iterations = 10000; // Number of iterations for PBKDF2
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Delimiter = ','; // Delimiter for storing salt and hash

    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        // Generate a secure random salt
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        // Create a hash using PBKDF2
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

        // Return the salt and hash as a single string
        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool VerifyPassword(string passwordHash, string inputPassword)
    {
        if (string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(inputPassword))
            return false;

        // Split the stored hash to get salt and hash
        var elements = passwordHash.Split(Delimiter);

        if (elements.Length != 2)
        {
            return false; // Invalid format
        }

        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        // Hash the input password with the same salt
        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);

        // Use fixed-time comparison to prevent timing attacks
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}
