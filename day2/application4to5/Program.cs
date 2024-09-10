namespace application4to5
{
    internal class Program
    {
        class Employee
        {
            public int EmployeeId { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public List<Expense> Expenses { get; set; } = new List<Expense>();
        }

        class Expense
        {
            public int ExpenseId { get; set; }
            public double Amount { get; set; }
            public string Description { get; set; }
            public Employee Employee { get; set; }
        }
        static string ctw(int num)

        {

            if (num == 0) return "Zero";



            string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",

               "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] thousands = { "", "One Thousand", "Two Thousand", "Three Thousand", "Four Thousand", "Five Thousand",

                    "Six Thousand", "Seven Thousand", "Eight Thousand", "Nine Thousand" };



            string result = "";



            if (num / 1000 > 0)

            {

                result += thousands[num / 1000] + " ";

                num %= 1000;

            }



            if (num / 100 > 0)

            {

                result += ones[num / 100] + " Hundred ";

                num %= 100;

            }



            if (num > 19)

            {

                result += tens[num / 10] + " ";

                num %= 10;

            }



            if (num > 0)

            {

                result += ones[num];

            }



            return result.Trim();

        }

        static void task4()

        {

            Console.WriteLine("task 4");

            
            //Console.WriteLine("Take input from user(max 9999) and print the number in words\r+\nexample\r\n1023 - " 
            //    "One Thousand and Twenty Three\r\n2841 -" +
            //    " Two Thousand Eight Hundred and Forty One");

            int number = int.Parse(Console.ReadLine());

            Console.WriteLine($"{number} in words: {ctw(number)}");

        }
        static void task5()

        {

            Console.WriteLine("task 5");
            //Console.WriteLine("Create and application that will be used for Expense Tracking of Employees in a " +
            //    "company\r\nCreate the Employee and the Expense class(Models) now with the appropriate attributes and behaviour");

            Employee emp = new Employee { EmployeeId = 1, Name = "John Doe", Department = "Finance" };
            Expense expense1 = new Expense { ExpenseId = 101, Amount = 200, Description = "Travel Expenses", Employee = emp };
            Expense expense2 = new Expense { ExpenseId = 102, Amount = 150, Description = "Lunch with Clients", Employee = emp };

            emp.Expenses.Add(expense1);
            emp.Expenses.Add(expense2);

            Console.WriteLine("Task 5: Employee and Expense Details");
            Console.WriteLine($"Employee: {emp.Name}, Department: {emp.Department}");
            Console.WriteLine("Expenses:");
            foreach (var exp in emp.Expenses)
            {
                Console.WriteLine($"Expense ID: {exp.ExpenseId}, Amount: {exp.Amount}, Description: {exp.Description}");
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, appl (4 &5) World!");
            //Console.WriteLine("enter 1 for task 4 ");
            //Console.WriteLine("enter 2 for task 5");

            //int num;
            // int number = int.Parse(Console.ReadLine());
            
            task4();
            Console.WriteLine("-----------------------------------------");
           
            task5();

            Console.ReadKey();


        }

    }
}
