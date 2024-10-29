using ConsoleApplication25Oct.ConstructorExample;

namespace ConsoleApplication25Oct
{
    internal class Program
    {

        public void ConstructorFunc()
        {
            

            // Default Constructor is Called
            // without parameters
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Default Constructor");
            Console.ResetColor(); 
            var defaultObj = new DefaultConstructor();
            Console.WriteLine($"Number: {defaultObj.Number}, Text: {defaultObj.Text}");

            


            // Parameterized constructor is called
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Parameterized Constructor");
            Console.ResetColor(); 
            var user = new UserProfile("Alice", "alice@example.com");
            Console.WriteLine($"User: {user.Name}, Email: {user.Email}");

            

            // Copy Constructor is called
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Copy Constructor");
            Console.ResetColor(); 
            var character1 = new GameCharacter("Barbie", 20);
            var character2 = new GameCharacter(character1); // Clone of Barbie Character
            character2.Name = "Clone Barbie"; // Modify clone independently
            Console.WriteLine($"Character 1: {character1.Name}, Health: {character1.Health}");
            Console.WriteLine($"Character 2: {character2.Name}, Health: {character2.Health}");


            

            // Static Constructor is called
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Static Constructor");
            Console.ResetColor(); 
            Console.WriteLine($"Database Connection: {DatabaseConfig.ConnectionString}");

            

            // Private Constructor is Called here!
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Private Constructor");
            Console.ResetColor();
            var logger = Logger.GetInstance();
            // This line calls the GetInstance() method to retrieve the
            // Logger instance. If it’s the first call,
            // a new Logger will be created; otherwise,
            // the existing instance will be returned.

            logger.Log("This is a log message.");
            // Here, the Log method is called on the logger instance to
            // log the message "This is a log message."
            // The message gets printed to the console


            
        }

        public void DestructorFunc()
        {
            // Destructor example
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Destructor Example");
            Console.ResetColor(); 
            ResourceHandler handler = new ResourceHandler();
            // Forcing garbage collection
            handler = null; // Remove reference to the object
            GC.Collect(); // Request garbage collection
            GC.WaitForPendingFinalizers(); // Wait for the finalizers to complete
            Console.WriteLine("Garbage collection has been requested.");
            // The ResourceHandler object will be cleaned up when it goes out of scope.
        }

        static void IDisposableUse()
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("IDisposable Example");
            Console.ResetColor(); 

            using (Employee employee = new Employee())
            {
                Console.WriteLine(employee.Name);             
            }
        }

        static void ConstructorChaning()
        {
            // Using the default constructor
            Car defaultCar = new Car();
            Console.WriteLine($"Default Car: {defaultCar.Make}, {defaultCar.Model}, {defaultCar.Year}, {defaultCar.Color}");

            // Using the constructor with make and model
            Car basicCar = new Car("Toyota", "Corolla");
            Console.WriteLine($"Basic Car: {basicCar.Make}, {basicCar.Model}, {basicCar.Year}, {basicCar.Color}");

            // Using the constructor with make, model, and year
            Car yearCar = new Car("Ford", "Mustang", 2021);
            Console.WriteLine($"Year Car: {yearCar.Make}, {yearCar.Model}, {yearCar.Year}, {yearCar.Color}");

            // Using the constructor with all parameters
            Car fullSpecCar = new Car("Tesla", "Model S", 2023, "Red");
            Console.WriteLine($"Full Spec Car: {fullSpecCar.Make}, {fullSpecCar.Model}, {fullSpecCar.Year}, {fullSpecCar.Color}");
        }

        public Program() 
        {
            //ConstructorFunc();
           // DestructorFunc();
            IDisposableUse();
            //ConstructorChaning();
        }
        static void Main(string[] args)
        {
            

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine("Hello, Welcome TO All Example! \n");
            Console.ResetColor(); 

            Program program = new Program();
     
        }
    }
}
