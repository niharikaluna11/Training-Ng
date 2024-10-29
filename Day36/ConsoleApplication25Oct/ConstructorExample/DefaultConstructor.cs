using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25Oct.ConstructorExample
{
    public class DefaultConstructor
    {
        /*When constructors do not have parameters, 
        then it is called the default constructor.
        These types of constructors have all its instance 
        initialized with the same value.*/

        public int Number;
        public string Text;

        // Default Constructor
        public DefaultConstructor()
        {
         //oi
         Number = 1;
        }
      
    }
}
