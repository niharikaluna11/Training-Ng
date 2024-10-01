using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Employee : IDisposable
    {
        //variables
        public string Name { get; set; }
        public int Id { get; set; }
        public List<string> Skills { get; set; }

        Leave leave = new Leave();


        //methods
        public string this[int index]
        {
            get { return Skills[index]; }
            set { Skills[index] = value; }
        }

        //indexer above

        //overloading indexer :)
        public int this[string skillname]
        {
           
            get
            {
                int idx = -1;
                for (int i = 0; i < Skills.Count; i++)
                {
                    if (Skills[i] == skillname)
                    {
                        idx = i;
                        break;
                    }
                }
                return idx;
            }

        }

        //constrtuctiuor
        public Employee()
        {
            Console.WriteLine("Employee created");
            Name = "Ramu";
            Skills = new List<string>() { "C#", "ASP.NET","ok", "MVC", "SQL Server" };
            leave.SickLeave = 10;
            leave.CasualLeave = 10;
        }

        // Destructor (finalizer)
        ~Employee()
        {
            Console.WriteLine("Employee destroyed");
        }

        //dispose method 
        public void Dispose()
        {
            Console.WriteLine("This is the dispose method");
            GC.SuppressFinalize(this); // Prevent finalizer from running if Dispose is called
        }
        public bool AvailSickLeave(int days)
        {
            if (leave.SickLeave >= days)
            {
                leave.SickLeave -= days;
                return true;
            }
            return false;
        }
        class Leave
        {
            public int CasualLeave { get; set; }
            public int SickLeave { get; set; }
        }

    }
}