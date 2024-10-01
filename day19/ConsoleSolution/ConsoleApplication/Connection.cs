using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Connection
    {
        public string ConnectionName { get; set; }
        private static Connection connection = null;

        //You cannot instaiate teh object of this calls coz the constrictor is private
        private Connection()
        {
                // making constructor private :)

        }
        public static Connection GetConnection()
        {
            if (connection == null)
            {
                connection = new Connection();
            }
            return connection;
        }

    }
}
