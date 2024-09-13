namespace App3
{
    internal class Program
    {
        static void DisplayMenu()
        {
            Console.WriteLine("Employee Management System");
            Console.WriteLine("1. Add a new employee");
            Console.WriteLine("2. Modify an employee's details");
            Console.WriteLine("2. Display all employees sorted by salary");
            Console.WriteLine("4. Print employee details by ID");
            Console.WriteLine("5. Delete an employee by ID");
            Console.WriteLine("6. Find employees by name");
            Console.WriteLine("7. Find employees older than a given employee");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");
        }

        static EmployeeService employeeService = new EmployeeService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                DisplayMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        employeeService.AddEmployee();
                        break;
                    case "2":
                        employeeService.ModifyEmployee();
                        break;
                    case "3":
                        DisplayEmployeesSortedBySalary();
                        break;
                    case "4":
                        FindAndDisplayEmployeeById();
                        break;
                    case "5":
                        employeeService.DeleteEmployee();
                        break;
                    case "6":
                        FindAndDisplayEmployeesByName();
                        break;
                    case "7":
                        FindAndDisplayEmployeesOlderThan();
                        break;
                    case "8":
                        return; // Exit the application
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }
        }

        static void FindAndDisplayEmployeesOlderThan()
        {
            Console.WriteLine("Enter the ID of the employee to compare age:");
            int idToCompare = Convert.ToInt32(Console.ReadLine());

            // Retrieve the employee with the given ID
            Employee referenceEmployee = employeeService.GetEmployeeById(idToCompare);

            if (referenceEmployee != null)
            {
                employeeService.FindAndDisplayEmployeesOlderThan(referenceEmployee);
            }
            else
            {
                Console.WriteLine($"No employee found with ID {idToCompare}.");
            }
        }

        static void DisplayEmployeesSortedBySalary()
        {
            List<Employee> employeeList = employeeService.GetAllEmployeesSortedBySalary();

            // Display sorted employees
            Console.WriteLine("\nEmployees sorted by salary:");
            foreach (var emp in employeeList)
            {
                Console.WriteLine(emp);
            }
        }

        static void FindAndDisplayEmployeeById()
        {
            Console.WriteLine("Enter the employee ID to search for:");
            int idToSearch = Convert.ToInt32(Console.ReadLine());
            employeeService.FindAndDisplayEmployeeById(idToSearch);
        }

        static void FindAndDisplayEmployeesByName()
        {
            Console.WriteLine("Enter the name to search for employees:");
            string nameToSearch = Console.ReadLine();
            List<Employee> employeesByName = employeeService.FindEmployeesByName(nameToSearch);

            if (employeesByName.Count > 0)
            {
                Console.WriteLine($"Employees with the name '{nameToSearch}':");
                foreach (var emp in employeesByName)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                Console.WriteLine($"No employees found with the name '{nameToSearch}'.");
            }
        }
    }
}
