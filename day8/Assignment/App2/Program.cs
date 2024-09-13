namespace App2
{
    internal class Program
    {
        static EmployeeService employeeService = new EmployeeService();
        static Employee employees = new Employee();  

        static void Main(string[] args)
        {
            Console.WriteLine("How many employees do you want to enter?");
            int numberOfEmployees = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numberOfEmployees; i++)
            {
                try
                {
                    Employee emp = new Employee();
                    emp.TakeEmployeeDetailsFromUser();
                    employeeService.AddEmployee(emp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--; // Retry for the current employee
                }
            }

               DisplayEmployeesSortedBySalary();

              FindAndDisplayEmployeeById();

               FindAndDisplayEmployeesByName();

               FindAndDisplayEmployeesOlderThan();
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
            List<Employee> employeeList = employeeService.GetAllEmployees();

            employeeList.Sort(); // Uses CompareTo method to sort by salary

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
            // Find employees by name
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
