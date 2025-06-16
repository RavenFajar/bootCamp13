using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DepartmentPortal.Models;
using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using DepartmentPortal.DTOs;
using DepartmentPortal.Interfaces;

namespace DepartmentPortal.Controllers;

public class DepartmentController : Controller
{
    private readonly IServiceDepartment _serviceDepartment;

    public DepartmentController(IServiceDepartment serviceDepartment)
    {
        _serviceDepartment = serviceDepartment;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(DepartmentCreateDto DepartmentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return View(DepartmentCreateDto);
        }
        var departmentDto = await _serviceDepartment.AddAsync(DepartmentCreateDto);
        return RedirectToAction("List", "Department");
        
        // var department = new Department
        // {
        //     Id = Guid.NewGuid(),
        //     Name = model.Name,
        //     Location = model.Location
        // };
        // _dbContext.Departments.AddAsync(department);
        // _dbContext.SaveChanges();
        // return RedirectToAction("List", "Department");

    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var departments = await _serviceDepartment.GetAllAsync();
        if (departments == null || !departments.Any())
        {
            return NotFound("No departments found.");
        }
        return View(departments);

        // var departments = await _dbContext.Departments.ToListAsync();
        // if (departments == null)
        // {
        //     return NotFound("No departments found.");
        // }
        // return View(departments);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        var result = await _serviceDepartment.DeleteAsync(id);
        if (!result)
        {
            return NotFound("Department not found.");
        }
        return RedirectToAction("List", "Department");

        // var department = await _dbContext.Departments.FindAsync(id);
        // if (department == null)
        // {
        //     return NotFound("Department not found.");
        // }
        // _dbContext.Departments.Remove(department);
        // await _dbContext.SaveChangesAsync();
        // return RedirectToAction("List", "Department");
    }
}