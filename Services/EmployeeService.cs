using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories.Interfaces;
using EmployeeManagement.Services.Interfaces;
using OfficeOpenXml;


namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IMapper mapper, IEmployeeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> ImportFromExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            ExcelPackage.License.SetNonCommercialOrganization("employees_100k_min");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            var employees = new List<Employee>();

            for (int row = 2; row <= rowCount; row++)
            {
                var name = worksheet.Cells[row, 1].Text;
                var dobText = worksheet.Cells[row, 2].Text;

                if (string.IsNullOrWhiteSpace(name) || !DateTime.TryParse(dobText, out var dob))
                    continue;

                employees.Add(new Employee
                {
                    FullName = name,
                    DateOfBirth = dob
                });
            }

            int lastId = _repository.GetEmployeeCount();
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].EmployeeCode = $"NV_{lastId + i + 1}";
            }

            await _repository.AddRangeAsync(employees);
            return employees.Count;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync(int page, int pageSize)
        {
            var data = await _repository.GetPagedAsync(page, pageSize);
            return _mapper.Map<List<EmployeeDto>>(data);
        }
    }

}
