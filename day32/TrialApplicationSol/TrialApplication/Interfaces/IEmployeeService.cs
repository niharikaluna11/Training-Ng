using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeDTO employee);

        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
