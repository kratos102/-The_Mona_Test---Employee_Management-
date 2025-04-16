using EmployeeManagement.Entities;

namespace EmployeeManagement.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddRangeAsync(IEnumerable<Employee> employees);
        Task<List<Employee>> GetPagedAsync(int page, int pageSize);
        int GetEmployeeCount();
    }
}
