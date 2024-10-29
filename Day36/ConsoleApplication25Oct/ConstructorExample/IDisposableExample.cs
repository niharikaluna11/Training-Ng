using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    internal class Employee : IDisposable
    {
        public string Name { get; set; }
       
        public Employee()
        {
            Console.WriteLine("Employee created");
            Name = "Niharika garg";
            
        }

    
        // Destructor (finalizer) for cleanup
      
        // Dispose method to release unmanaged resources
        public void Dispose()
        {
            Console.WriteLine("This is the dispose method");
        }
    }
}
