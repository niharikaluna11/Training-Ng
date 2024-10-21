using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Context;
using TrialApplication.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TrialApplication.Repositories
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly EventBookingContext _context;

        public EmployeeRepository(EventBookingContext eventbookingContext)
        {
            _context = eventbookingContext;
        }

        public async Task<Employee> Get(int key)
        {
            var emp = _context.Employees.FirstOrDefault(c => c.Id == key);
            return emp;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var emp = await _context.Employees.ToListAsync();
            if (emp.Count == 0)
            {
                throw new CollectionEmptyException("Employee");
            }
            return emp;
        }

      
        public async Task<Employee> Add(Employee entity)
        {
            try
            {
                _context.Employees.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new CouldNotAddException("Employee");
            }
        }

        Task<Employee> IRepository<int, Employee>.Delete(int key)
        {
            throw new NotImplementedException();
        }

      
        Task<Employee> IRepository<int, Employee>.Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
