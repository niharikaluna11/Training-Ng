namespace Assignment4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //Take number from user until 0 is entered. Print all the prime numbers entered  

            PrimeNumber primeChecker = new PrimeNumber();

            Console.WriteLine("Enter numbers (enter 0 to stop):");

            List<int> numbers = new List<int>();

            // Read numbers from the user until 0 is entered
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number) && number != 0)
                {
                    numbers.Add(number);
                }
                else if (number == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            // Print the prime numbers
            List<int> primeNumbers = primeChecker.GetPrimeNumbers(numbers);
            Console.WriteLine("Prime numbers entered:");
            foreach (int prime in primeNumbers)
            {
                Console.WriteLine(prime);
            }
        
    }
    }
}
