using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace CustomerPictureManagementSystem
{
    //customer management system
   /* Take Picture path from user and Create a folder in a specific location with the CUstomer name
    Store all the pictures given by customer in the location
    When another customer gives data the same process should be repeated*/

    public class Program
    {
        //displaycolormessage function 
        internal static void DisplayColoredMessage(string message, string foreground, string background)
        {
            if (Enum.TryParse(foreground, true, out ConsoleColor fgColor))
            {
                Console.ForegroundColor = fgColor;
            }

            if (Enum.TryParse(background, true, out ConsoleColor bgColor))
            {
                Console.BackgroundColor = bgColor;
            }

            Console.WriteLine($"{message}");
            Console.ResetColor();
        }
       
        //run method to do main things ok:)
        public void Run()
        {
            DisplayColoredMessage("Hello, Welcome To the Customer Picture Management Application!", "black", "cyan");

            //creating object of customer 
            Customer customer = new Customer();
            //calling the only one function
            customer.EnterDetails();
        }

        //main method 
        static void Main(string[] args)
        {
          Program program = new Program();
          program.Run();
          Console.ReadKey();
                    
        }

    }
}

