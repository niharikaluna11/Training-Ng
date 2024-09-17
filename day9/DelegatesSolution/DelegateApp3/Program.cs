namespace DelegateApp3
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
                new Employee { ID = 101, Name = "Mark", Salary = 50100 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 141000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };
                //Console.WriteLine("Please enter the employee Name");
                //string name = Console.ReadLine();

            //Language Integrated Query (LINQ)
            //var result = (from e in employees
            //              where e.Name == name
            //              select e);
            //var employee = result.FirstOrDefault();//The first value in teh collection or null which is the default for referebnce type

            //var employee=employees.FirstOrDefault(e=>e.Name==name);
            var highpaidEmp = employees.OrderBy(emp => emp.Salary);
            foreach (Employee emp in highpaidEmp)
            { 
                Console.WriteLine(emp);
            }
                //if (employee != null)
                //    Console.WriteLine(employee);
                //else
                //    Console.WriteLine("Employee not found");

            }

            //public bool FindEmployee(Employee employee)
            //{
            //    return employee.Name == "John";
            //}
            static void Main(string[] args)
            {
                new Program();
            Console.ReadKey();
            }
        }
    }
