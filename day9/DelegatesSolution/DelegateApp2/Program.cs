namespace DelegateApp2
{
    
        class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Salary { get; set; }
            public override string ToString()
            {
                return "Id = " + ID + ", Name = " + Name + ", Salary = " + Salary;
            }
        }
        internal class Program
        {
            public Program()
            {
                UnderstandingTheUseOfDelegate();
            }
            void UnderstandingTheUseOfDelegate()
            {
                List<Employee> employees = new List<Employee>()
            {
                new Employee { ID = 101, Name = "Mark", Salary = 5000 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 14000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };
                Console.WriteLine("Please enter the employee Name");
                string name = Console.ReadLine();
                //Employee employee = null;
                //for (int i = 0; i < employees.Count; i++)
                //{
                //    if (employees[i].Name==name)
                //        employee = employees[i];
                //}
                // Predicate<Employee> employeePredicate = new Predicate<Employee>(e=>e.Name==name);//Lambda Expression
                Employee employee = employees.Find(new Predicate<Employee>(e => e.Name == name));
                if (employee != null)
                    Console.WriteLine(employee);
                else
                    Console.WriteLine("Employee not found");
            }
            //public bool FindEmployee(Employee employee)
            //{
            //    return employee.Name == "John";
            //}
            static void Main(string[] args)
            {
                new Program();
            }
        }
    }
