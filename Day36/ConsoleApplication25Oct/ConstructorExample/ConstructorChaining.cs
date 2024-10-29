using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    public class Car
    {
        public string Make;
        public string Model;
        public int Year;
        public string Color;

        // Default constructor with default values
        public Car() : this("Unknown Make", "Unknown Model", 2022, "Black") { }

        // Constructor with specified make and model
        public Car(string make, string model) : this(make, model, 2022, "Black") { }

        // Constructor with specified make, model, and year
        public Car(string make, string model, int year) : this(make, model, year, "Black") { }

        // Main constructor where all parameters are provided
        public Car(string make, string model, int year, string color)
        {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
        }
    }

}
