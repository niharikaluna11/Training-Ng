using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3
{
    internal class EmployeeService
    {
        private Dictionary<int, Employee> employeeDictionary = new Dictionary<int, Employee>();

        // Method to add an employee
        public void AddEmployee()
        {
            Console.WriteLine("Enter Employee ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Employee Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Employee Salary:");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter Employee Age:");
            int age = Convert.ToInt32(Console.ReadLine());

            Employee employee = new Employee
            {
                Id = id,
                Name = name,
                Salary = (double)salary,
                Age = age
            };

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

        // Method to modify an employee's details
        public void ModifyEmployee()
        {
            Console.WriteLine("Enter Employee ID to modify:");
            int id = Convert.ToInt32(Console.ReadLine());

            if (employeeDictionary.TryGetValue(id, out Employee employee))
            {
                Console.WriteLine("Enter new Employee Name:");
                employee.Name = Console.ReadLine();

                Console.WriteLine("Enter new Employee Salary:");
                employee.Salary = (double)Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Enter new Employee Age:");
                employee.Age = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Employee details updated successfully.");
            }
            else
            {
                Console.WriteLine($"No employee found with ID {id}.");
            }
        }

        // Method to delete an employee by ID
        public void DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID to delete:");
            int id = Convert.ToInt32(Console.ReadLine());

            if (employeeDictionary.Remove(id))
            {
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine($"No employee found with ID {id}.");
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

        // Method to find and display employees by name
        public List<Employee> FindEmployeesByName(string name)
        {
            return employeeDictionary.Values
                .Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Method to find and display employees older than a given employee
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

        // Method to find and display employee by ID
        public void FindAndDisplayEmployeeById(int id)
        {
            var employee = GetEmployeeById(id);

            if (employee != null)
            {
                Console.WriteLine("Employee details:");
                Console.WriteLine(employee);
            }
        }

        // Method to get all employees sorted by salary
        public List<Employee> GetAllEmployeesSortedBySalary()
        {
            List<Employee> employeeList = new List<Employee>(employeeDictionary.Values);
            employeeList.Sort(); // Uses CompareTo method to sort by salary
            return employeeList;
        }

        // Method to get all employees
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(employeeDictionary.Values);
        }
    }
}