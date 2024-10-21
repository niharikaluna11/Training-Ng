using AutoMapper;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int, Employee> employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;

        }

        public async Task<int> CreateEmployee(EmployeeDTO employee)
        {
           
            Employee newEmployee = _mapper.Map<Employee>(employee);
            var addedEmployee = await _employeeRepo.Add(newEmployee);
            return addedEmployee.Id;
        }


        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var emp = await _employeeRepo.GetAll();
                return emp;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Employees");
            }
        }


    }
}
