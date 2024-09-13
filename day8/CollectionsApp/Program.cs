using System.Collections;

namespace CollectionsApp
{

    internal class Program
    {
        void UnderstandingCollection()
        {
            ArrayList numbers = new ArrayList();
            numbers.Add(100);
            numbers.Add(234);
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Add("Hello");
            int sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum += Convert.ToInt32(numbers[i]);
            }
            Console.WriteLine(sum);
        }
        void UnderstandingList()
        {
            List<int> numbers = new List<int>();//generic collection - typesafe
            numbers.Add(100);
            //numbers.Add("Hello");//not possible
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(new Random().Next(i, 100));
            }
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
        void UnderstaingMoreOnList()
        {
            List<Customer> customers = new List<Customer>();
            int choice = 0;
            do
            {
                Customer customer = new Customer();
                customer.GetCustomerDetaislFromConsole();
                customer.Id = customers.Count + 100;
                customers.Add(customer);
                Console.WriteLine("Do you want to continue? Then enter any number otehr than 0.");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice != 0);
            Console.WriteLine("----------------------------------------");

            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer);
            //}   
            //Console.WriteLine(customers[0]);//We can access the elements using index
            bool isCustomerFound = customers.Contains(new Customer(100, "Ramu", 1234567890));
            Console.WriteLine("Is Ramu present " + isCustomerFound);

        }
        void UnderstaningLimitationOfArray()
        {
            int[] numbers = new int[10];
            for (int i = 0; i < 10; i++)
            {
                numbers[i] = i * 100 + new Random().Next(10, 100);
            }
            //To increase the size of array we have to create a new array and copy the old array to new array
            int[] nums1 = new int[12];
            for (int i = 0; i < 10; i++)
            {
                nums1[i] = numbers[i];
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            // program.UnderstaningLimitationOfArray();
            //program.UnderstandingCollection();
            //program.UnderstandingList();
            program.UnderstaingMoreOnList();
        }
    }
}
