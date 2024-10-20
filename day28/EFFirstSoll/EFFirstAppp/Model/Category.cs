using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFirstAppp.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } 
            = string.Empty;

        // 1 category have many product

        public IEnumerable<Product> Products { get; set; }

    }
}
