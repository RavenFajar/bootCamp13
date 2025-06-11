using EmployeeAdmin.Data;
using EmployeeAdmin.Models;
using EmployeeAdmin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationtDBContext dBContext;

        public EmployeesController(ApplicationtDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dBContext.Employees.ToList();
            return Ok(allEmployees);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeDto addEmployeDto) {

            var employeEntiti = new Employee()
            {
                Name = addEmployeDto.Name,
                Email = addEmployeDto.Email,
                Salary = addEmployeDto.Salary,
            };

            dBContext.Employees.Add(employeEntiti);
            dBContext.SaveChanges();
            return Ok(employeEntiti);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dBContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto ) {

            var employee = dBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Salary = updateEmployeeDto.Salary;

            dBContext.SaveChanges();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteEmploye(Guid id) {
            var employee = dBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            dBContext.Employees.Remove(employee);
            dBContext.SaveChanges();
            return Ok(employee );
        }
    }
}
