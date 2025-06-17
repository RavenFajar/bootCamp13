using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DepartmentPortal.Models;
using DepartmentPortal.Data;
using DepartmentPortal.Interfaces;
using DepartmentPortal.DTOs;
using DepartmentPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepartmentPortal.Controllers;

public class EmployeeController : Controller
{
    private readonly IServiceEmployee _employeeService;

    public EmployeeController(IServiceEmployee employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Departments = await _employeeService.GetDepartmentsAsSelectListAsync();
        return View();

        // var departments = await _dbContext.Departments.ToListAsync();
        // ViewBag.Departments = departments.Select(d => new SelectListItem
        // {
        //     Value = d.Id.ToString(),
        //     Text = d.Name
        // }).ToList();
        // return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(EmployeeCreateDto EmployeeCreateDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _employeeService.GetDepartmentsAsSelectListAsync();
            return View(EmployeeCreateDto);
        }

        await _employeeService.AddAsync(EmployeeCreateDto);
        return RedirectToAction("Index", "Home");

        // var employee = new Employee
        // {
        //     Id = Guid.NewGuid(),
        //     Name = model.Name,
        //     Position = model.Position,
        //     HireDate = model.HireDate,
        //     Salary = model.Salary,
        //     Department = _dbContext.Departments.FirstOrDefault(d => d.Id == model.DepartmentId)

        // };
        // _dbContext.Employees.AddAsync(employee);
        // _dbContext.SaveChanges();
        // return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var employee = await _employeeService.GetAllAsync();
        if (employee == null || !employee.Any())
        {
            return NotFound("No employee found.");
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        Console.WriteLine($"Received ID: {id}");
        var result = await _employeeService.DeleteAsync(id);
        if (!result)
        {
            return NotFound("Employee not found.");
        }
        return RedirectToAction("List", "Employee");
    }
}