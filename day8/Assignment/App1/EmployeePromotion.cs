using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    public class EmployeePromotion 
    {
        public List<string> employees { get; set; } = new List<string>(); //(List)

        public void InputEmployee()
        {
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion (Please enter blank to stop)");
            //take employee details in order
            while (true)
            {
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }
                employees.Add(name); //employyee name is added

            }

        }
        public void PromotionList()
        {
            Console.WriteLine("employee list");
            foreach (string employee in employees)
            {
                Console.WriteLine(employee);
            }
        }
      
        public void PositionPromotion()
        {
            Console.WriteLine("Please enter the name of the employee to check promotion position:");
            string name = Console.ReadLine();
            int position = -1;

            for (int i = 0; i < employees.Count; i++)
            {
                // Display employee name and index
                Console.WriteLine(employees[i] + ": " + (i + 1));

                if (employees[i] == name)
                {
                    position = i + 1;
                }
            }

            if (position > 0)
            {
                Console.WriteLine($"\"{name}\" is in position {position} for promotion.");
            }
            else
            {
                Console.WriteLine($"\"{name}\" is not found in the promotion list.");
            }
        }
        public void TrimExcess()
        {
            //InputEmployee();
            // Display the current size of the collection (capacity)
            Console.WriteLine($"The current size of the collection is {employees.Capacity}");

            // Trim the list to only use as much memory as necessary
            employees.TrimExcess();

            // Display the new size of the collection (capacity should match the count now)
            Console.WriteLine($"The size after removing the extra space is {employees.Count}");


        }
        public void SortNames()
        {
            employees.Sort();

            Console.WriteLine("Promoted employee list in ascending order:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

}
}
