using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    // Custom exception class
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
    public class Login
    {

        private string validUsername = "ABC";
        private string validPassword = "123";
        private int maxAttempts = 3;

        public void CheckLogin()
        {
            int attempts = 0;
            while (attempts < maxAttempts)
            {
                try
                {
                    Console.Write("Enter Username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();

                    if (username == validUsername && password == validPassword)
                    {
                        Console.WriteLine("Login successful!");
                        return;
                    }
                    else
                    {
                        attempts++;
                        if (attempts == maxAttempts)
                        {
                            throw new LoginException("You have exceeded the number of attempts. Login blocked.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password. Attempts left: " + (maxAttempts - attempts));
                        }
                    }
                }
                catch (LoginException ex)
                {
                    Console.WriteLine(ex.Message);
                    break; // stop further execution after exception is thrown
                }
            }
        }
    }
}
