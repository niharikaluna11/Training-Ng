using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication25Oct.ConstructorExample
{
    /*When creating a user profile
    , you may want to initialize user 
    attributes such as name and email during instantiation.*/
    public class UserProfile
    {
        public string Name { get; }
        public string Email { get; }

        // Parameterized Constructor
        public UserProfile(string name, string email)
        {
            Name = name;
            Email = email;
        }

        ~UserProfile()
        {
            // Cleanup code here
            Console.WriteLine("Destructor called: UserProfile instance destroyed.");
        }
    }
}
