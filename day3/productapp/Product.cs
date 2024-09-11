using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public int SupplierID { get; set; } //foreign keyyy
        public int CustomerID { get; set; }//foreign keyyy
    }
}
