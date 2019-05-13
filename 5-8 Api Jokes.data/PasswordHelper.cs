using System;
using System.Collections.Generic;
using System.Text;

namespace _5_8_Api_Jokes.data
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool PasswordMatch(string userInput, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(userInput, passwordHash);
        }
    }
}
