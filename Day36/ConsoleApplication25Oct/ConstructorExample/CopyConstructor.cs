using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication25Oct.ConstructorExample
{
    public class GameCharacter
    {
       //  When working with game characters,
       // you may want to create a clone of an existing
       // character with the same attributes but allow both
       // instances to operate independently.
        public string Name { get; set; }
        public int Health { get; set; }

        // Copy Constructor
        public GameCharacter(GameCharacter original)
        {
            Name = original.Name;
            Health = original.Health;
        }

        // Parameterized Constructor
        public GameCharacter(string name, int health)
        {
            Name = name;
            Health = health;
        }
        ~GameCharacter()
        {
            // Cleanup code here
            Console.WriteLine("Destructor called: GameCharacter instance destroyed.");
        }
    }
}
