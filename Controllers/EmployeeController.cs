using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest("No file uploaded.");

            var count = await _employeeService.ImportFromExcelAsync(file);
            return Ok(new { message = $"{count} employees imported successfully" });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(int page = 1, int pageSize = 20)
        {
            var result = await _employeeService.GetEmployeesAsync(page, pageSize);
            return Ok(result);
        }
    }

}
