using BCrypt.Net;
using System;

namespace Clinic.API.Features.Auth.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Generate a salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Verify the provided password against the hashed password
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
