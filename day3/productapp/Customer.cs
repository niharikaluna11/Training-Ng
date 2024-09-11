using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    public class Customer : ICustomerRegis
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } // Added PhoneNumber property

        private static List<Customer> registeredCustomers = new List<Customer>();

     
        public void RegisterCustomer(Customer customer)
        {
            registeredCustomers.Add(customer);
            Console.WriteLine($"Customer {customer.CustomerID} has been registered.");
        }
        
        // Login customer
        public Customer LoginCustomer(int cId)
        {
            return registeredCustomers.FirstOrDefault(c => c.CustomerID == cId);
        }
        public static List<Customer> GetAllCustomers()
        {
            return registeredCustomers;
        }
    }
}
