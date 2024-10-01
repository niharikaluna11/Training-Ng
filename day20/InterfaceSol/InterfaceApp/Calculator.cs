using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApp
{
    public class Calculator : Interface1, Interface2
    {
        // Trying to implement both interfaces with same method signatures
        // causes ambiguity, hence explicit implementation is required.

        // Explicit implementation for Interface1's Sum method
        int Interface1.Sum(int a, int b)
        {
            return a + b;
        }

        // Explicit implementation for Interface2's Sum method
        int Interface2.Sum(int a, int b)
        {
            return a * b;  // Just to differentiate, multiplying the numbers
        }

        // Explicit implementation for Interface1's Display method
        void Interface1.Display()
        {
            Console.WriteLine("Displaying result from Interface1.");
        }

        // Explicit implementation for Interface2's Display method
        void Interface2.Display()
        {
            Console.WriteLine("Displaying result from Interface2.");
        }

        // Optionally, you can create a common method to access both interface methods
        public void ShowResults()
        {
            Console.WriteLine("Interface1: " + ((Interface1)this).Sum(5, 3)); // Calls Sum from Interface1
            Console.WriteLine("Interface2: " + ((Interface2)this).Sum(5, 3)); // Calls Sum from Interface2
        }
    }

}
