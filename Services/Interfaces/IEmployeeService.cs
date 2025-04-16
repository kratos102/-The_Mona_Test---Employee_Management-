using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> ImportFromExcelAsync(IFormFile file);
        Task<List<EmployeeDto>> GetEmployeesAsync(int page, int pageSize);
    }
}
