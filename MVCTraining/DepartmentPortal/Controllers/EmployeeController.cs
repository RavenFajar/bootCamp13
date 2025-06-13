using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DepartmentPortal.Models;
using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepartmentPortal.Controllers;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeController(ApplicationDbContext context)
    {
        this._dbContext = context;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var departments = await _dbContext.Departments.ToListAsync();
        ViewBag.Departments = departments.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name
        }).ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddEmployee model)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Position = model.Position,
            HireDate = model.HireDate,
            Salary = model.Salary,
            Department = _dbContext.Departments.FirstOrDefault(d => d.Id == model.DepartmentId)

        };
        _dbContext.Employees.AddAsync(employee);
        _dbContext.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}