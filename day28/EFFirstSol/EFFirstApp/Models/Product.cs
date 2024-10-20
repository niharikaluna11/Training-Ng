using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFirstApp.Models
{
    public class Product
    {
        public int Id { get; set; }//by default primary key, Also Idenetity with starting number 1 and increment 1
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } = string.Empty;
    }


}
