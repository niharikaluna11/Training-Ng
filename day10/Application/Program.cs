namespace Application
{
    internal class Program
    {
        void UnderstandingNullables()
        {
            int num1 = 10;
            int? num2 = null;//nullable primitive type
            //num1 = num2;//Not possible without casting
            //We can use this instead
            num1 = num2 ?? 0; //if num2 is null, assign 0 to num1
            num2 = num1; //possible- Implicit conversion
        }
        void UnderstandingLimits()
        {
            int num1 = int.MaxValue;
            Console.WriteLine($"The value of num1 is {num1}");
            checked//will throw an exception if overflow occurs
            {
                try
                {
                    num1++;
                    Console.WriteLine($"The value of num1 after incrementing is {num1}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Overflow occurred!");
                }
            }
        }
        void UndertandingErrorHandling()
        {
            int num1 = 0;
            Console.WriteLine("Please enter a number");

            //try
            //{
            //    num1 = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine($"The value you have entered is {num1}");
            //}
            //catch (FormatException fe)
            //{
            //    Console.WriteLine("Invalid entry for number");
            //}
            /*var result = int.TryParse(Console.ReadLine(), out num1);
            if (result)
            {
                Console.WriteLine($"The value you have entered is {num1}");
            }
            else
            {
                Console.WriteLine("Invalid entry for number");
            }*/

            while (int.TryParse(Console.ReadLine(), out num1) == false)
            {
                Console.WriteLine("Invalid entry for number. Try again");
            }
            Console.WriteLine($"The value you have entered is {num1}");

        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.UndertandingErrorHandling();
        }
    }
}
