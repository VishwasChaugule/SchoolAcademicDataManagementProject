using System.Security.Cryptography;
using System.Text;

namespace SchoolAcademicDataManagement.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from the password
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return string.Equals(inputPasswordHash, storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}

