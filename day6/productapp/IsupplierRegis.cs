using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    public interface IsupplierRegis
    {
       

        //supplier registration

        void RegisterSupplier(Supplier supplier);
        Supplier LoginSupplier(int supplierId); // Returns a Supplier object if found


    }
}
