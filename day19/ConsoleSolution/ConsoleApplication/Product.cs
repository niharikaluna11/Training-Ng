using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Product
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public int Id{ get; set; }

        public static Product operator +(Product a, Product b)
        {
            Product product = new Product();
            product.Id = a.Id;
            product.Name = a.Name+" and "+b.Name;
            product.Price = a.Price+b.Price;
            return product;
        }
        public override string ToString()
        {
            return "Id: " + Id + "\nName: " + Name + "\nPrice: $" + Price;
        }
    }
}
