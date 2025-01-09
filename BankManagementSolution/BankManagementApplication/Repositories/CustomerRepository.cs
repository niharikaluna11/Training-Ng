using BankManagementApplication.Context;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApplication.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankManagementContext _context;

        public CustomerRepository(BankManagementContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer entity)
        {
            try
            {
                await _context.Customers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred .", ex);
            }
        }

        
        public async Task<Customer> Delete(Customer entity, int key)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(key);

                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found.");
                }

                // Soft delete by changing status
                customer.CStatus = CustomerStatus.Deactivated;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred.", ex);
            }
        }

        public async Task<Customer> Get(int key)
        {
            try
            {
                return await _context.Customers.FindAsync(key);
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred.", ex);
            }
        }


        public async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _context.Customers
                                     .Where(c => c.CStatus == CustomerStatus.Activated) 
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred .", ex);
            }
        }



        public async Task<IEnumerable<Customer>> GetByAccountType(string accountType)
        {
            throw new NotImplementedException("not");
        }

        // Get customers by first & last name
        public async Task<IEnumerable<Customer>> GetByFName(string firstName)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.FirstName.Contains(firstName))
                    .Where(c => c.CStatus == CustomerStatus.Activated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred ", ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetByLName(string lastName)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.LastName.Contains(lastName))
                    .Where(c => c.CStatus == CustomerStatus.Activated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred .", ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAccountNumber(string accountNumber)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.AccountNumber.Contains(accountNumber))
                    .Where(c => c.CStatus == CustomerStatus.Activated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred ", ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersByPhoneNumber(string phoneNumber)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.PhoneNumber.Contains(phoneNumber))
                    .Where(c => c.CStatus == CustomerStatus.Activated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error occurred .", ex);
            }
        }


        public async Task<Customer> Update(Customer entity, int key)
        {
            try
            {
                var existingCustomer = await Get(key);

                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found.");
                }

                
                _context.Entry(existingCustomer).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return existingCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception(" error occurred while updating the customer.", ex);
            }
        }
    }
}
