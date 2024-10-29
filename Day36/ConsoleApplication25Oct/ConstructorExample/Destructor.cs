using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    // Destructor Example Class
    public class ResourceHandler
    {
        public ResourceHandler()
        {
            Console.WriteLine("ResourceHandler instance created.");
        }

        ~ResourceHandler()
        {
            // Cleanup code here
            Console.WriteLine("Destructor called: ResourceHandler instance destroyed.");
        }
    }
}
