using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    public class Logger
    {
        private static Logger instance; // 1

        // Private Constructor
        private Logger() { } 

        //The constructor is marked as private, meaning
        //no other class can create an instance of Logger
        //using the new keyword. This is crucial for the Singleton pattern,
        //as it prevents other classes from instantiating new objects.
        public static Logger GetInstance() // 3
            //public static Logger GetInstance()
            //This method provides a way to access the
            //single instance of the Logger class.
            //Since it is static, it can be called without
            //creating an object of the class.
        {
            if (instance == null) 
            {
                instance = new Logger(); 
            }
            return instance; 
        }

        public void Log(string message) 
        {
            Console.WriteLine(message); 
        }

        ~Logger()
        {
            // Cleanup code here
            Console.WriteLine("Destructor called: Logger instance destroyed.");
        }
    }

}
