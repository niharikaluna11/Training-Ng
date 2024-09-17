namespace UnderstandingTuples
{
    internal class Program
    {
        void UnderstandingTuples()
        {
            var person = (1, "John", "Doe");//Tuple can hold any datatype
            Console.WriteLine(person.Item1);
            Console.WriteLine(person.Item2);
            Console.WriteLine(person.Item3);
            //Named tuples
            var person1 = (Id: 1, FirstName: "John", LastName: "Doe");
            Console.WriteLine(person1.Id);
            Console.WriteLine(person1.FirstName);
            Console.WriteLine(person1.LastName);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();
            program.UnderstandingTuples();
        }
    }
}
