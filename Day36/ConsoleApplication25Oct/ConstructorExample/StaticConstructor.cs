using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    //Scenario: When you have a class that holds
    //configuration settings that only need to be
    //initialized once, a static constructor is useful.
    public class DatabaseConfig
    {
        public static string ConnectionString { get; private set; }

        // Static Constructor
        static DatabaseConfig()
        {
            ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        }

        ~DatabaseConfig()
        {
            // Cleanup code here
            Console.WriteLine("Destructor called: DatabaseConfig instance destroyed.");
        }
    }

}
