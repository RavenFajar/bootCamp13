using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DepartmentPortal.Models;
using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using DepartmentPortal.DTOs;
using DepartmentPortal.Interfaces;
using FluentValidation;

namespace DepartmentPortal.Controllers;

public class DepartmentController : Controller
{
    private readonly IServiceDepartment _serviceDepartment;
    private readonly IValidator<DepartmentCreateDto> _departmentValidator;

    public DepartmentController(IServiceDepartment serviceDepartment, IValidator<DepartmentCreateDto> departmentValidator)
    {
        _serviceDepartment = serviceDepartment;
        _departmentValidator = departmentValidator;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(DepartmentCreateDto DepartmentCreateDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                return View(DepartmentCreateDto);
            }

            await _serviceDepartment.AddAsync(DepartmentCreateDto);
            return RedirectToAction("List", "Department");

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error creating student: " + ex.Message);

            return View(DepartmentCreateDto);
        }
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
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        Console.WriteLine($"Received ID: {id}");
        var result = await _serviceDepartment.DeleteAsync(id);
        if (!result)
        {
            return NotFound("Department not found.");
        }
        return RedirectToAction("List", "Department");
    }

    [HttpGet]
    public async Task<IActionResult> Detail(Guid id)
    {
        var result = await _serviceDepartment.GetDepartmentByIdAsync(id);
        if (result == null)
        {
            return NotFound("Department not found.");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _serviceDepartment.GetDepartmentByIdAsync(id);
        if (result == null)
        {
            return NotFound("Department not found.");
        }
        return View(result);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }

            try
            {
                var updatedDepartment = await _serviceDepartment.UpdateAsync(departmentDto);
                if (updatedDepartment == null)
                {
                    return NotFound("Department not found.");
                }

                TempData["Success"] = "Department updated successfully!";
                return RedirectToAction("List");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating the department.");
                return View(departmentDto);
            }
        }
}