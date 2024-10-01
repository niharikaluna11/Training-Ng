namespace InterfaceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            // Resolving ambiguity by casting the object to the appropriate interface
            Interface1 int1 = calculator;
            Interface2 int2 = calculator;

            // Calling methods from Interface1
            Console.WriteLine("Sum from Interface1: " + int1.Sum(10, 20));  // Sum method from Interface1
            int1.Display();  // Display method from Interface1

            // Calling methods from Interface2
            Console.WriteLine("Sum from Interface2: " + int2.Sum(10, 20));  // Sum method from Interface2
            int2.Display();  // Display method from Interface2

            // Optional: calling both results via a common method in the class
            calculator.ShowResults();
        }
    }

}
