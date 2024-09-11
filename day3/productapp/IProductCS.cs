using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    internal interface IProductCS
    {
        void ListAllProducts(List<Supplier> suppliers);
        void PurchaseProduct(List<Supplier> suppliers, int productId, int quantity);
    }
}
