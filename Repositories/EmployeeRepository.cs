using EmployeeManagement.Data;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Employee> employees)
        {
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Employees
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public int GetEmployeeCount()
        {
            return _context.Employees.Count();
        }
    }

}
