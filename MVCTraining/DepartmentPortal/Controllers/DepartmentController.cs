using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DepartmentPortal.Models;
using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentPortal.Controllers;

public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    public DepartmentController(ApplicationDbContext context)
    {
        this._dbContext = context;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(AddDepartment model)
    {
        var department = new Department
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Location = model.Location
        };
        _dbContext.Departments.AddAsync(department);
        _dbContext.SaveChanges();
        return RedirectToAction("List", "Department");
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var departments = await _dbContext.Departments.ToListAsync();
        if (departments == null)
        {
            return NotFound("No departments found.");
        }
        return View(departments);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        var department = await _dbContext.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound("Department not found.");
        }
        _dbContext.Departments.Remove(department);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("List", "Department");
    }
}