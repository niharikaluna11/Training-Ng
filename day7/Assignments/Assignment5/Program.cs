namespace Assignment5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            CardValidator validator = new CardValidator();

            Console.WriteLine("enter card number ex.4477 4683 4311 3002 ");
            //"4477 4683 4311 3002"; // Example card number
            string cardNumber =Console.ReadLine();

            if (validator.Validate(cardNumber))
            {
                Console.WriteLine("The card number is valid.");
            }
            else
            {
                Console.WriteLine("The card number is invalid.");
            }
        }
    }
}
