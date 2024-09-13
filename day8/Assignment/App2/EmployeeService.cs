namespace App2
{
    internal class EmployeeService
    {

        private Dictionary<int, Employee> employeeDictionary = new Dictionary<int, Employee>();

        // Method to add employee to dictionary
        public void AddEmployee(Employee employee)
        {
            if (employee.Id == 0)
            {
                Console.WriteLine("Error: Employee ID cannot be null or zero.");
                return;
            }

            if (employeeDictionary.ContainsKey(employee.Id))
            {
                Console.WriteLine($"Error: Employee ID {employee.Id} already exists.");
            }
            else
            {
                employeeDictionary.Add(employee.Id, employee);
                Console.WriteLine("Employee added successfully.");
            }
        }
        public List<Employee> FindEmployeesByName(string name)
        {
            List<Employee> matchingEmployees = new List<Employee>();

            foreach (var employee in employeeDictionary.Values)
            {
                if (employee.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    matchingEmployees.Add(employee);
                }
            }

            return matchingEmployees;
        }
        public void FindAndDisplayEmployeesOlderThan(Employee referenceEmployee)
        {
            var olderEmployees = employeeDictionary.Values
                .Where(e => e.Age > referenceEmployee.Age)
                .ToList();

            if (olderEmployees.Count > 0)
            {
                Console.WriteLine("Employees older than the given employee:");
                foreach (var emp in olderEmployees)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                Console.WriteLine("No employees found who are older than the given employee.");
            }
        }
        public void FindAndDisplayEmployeeById(int id)
        {
            var employee = employeeDictionary.Values
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (employee != null)
            {
                Console.WriteLine("Employee details:");
                Console.WriteLine(employee);
            }
            else
            {
                Console.WriteLine($"Employee with ID {id} not found.");
            }
        }

        // Method to retrieve employee by ID
        public Employee GetEmployeeById(int id)
        {
            if (employeeDictionary.TryGetValue(id, out Employee employee))
            {
                return employee;
            }
            else
            {
                Console.WriteLine($"Employee with ID {id} not found.");
                return null;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(employeeDictionary.Values);
        }
        
    }
}