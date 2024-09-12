using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    public interface ICustomerRegis
    {
        //customer registration
        void RegisterCustomer(Customer customer);
        Customer LoginCustomer(int customerId);
       
    }
}
