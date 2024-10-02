using System.Security.Cryptography;
using System.Text;



namespace SBRSystem_API.Services;

public class PasswordService
{
    public (string Hash, string Salt) HashPassword(string password)
    {
        // Generar un salt aleatorio
        var saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        var salt = Convert.ToBase64String(saltBytes);

        // Generar el hash combinando la contraseña con el salt
        var hash = ComputeHash(password, salt);

        return (Hash: hash, Salt: salt);
    }

    
    public string ComputeHash(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
