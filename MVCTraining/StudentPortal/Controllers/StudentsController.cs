using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models.Entities;
using StudentPortal.Models;
using StudentPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentPortal.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDBContext dbContext;

    public StudentsController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public Task<IActionResult> Add()
    {
        var model = new AddStudentsModel
        {
            Name = string.Empty,
            Email = string.Empty,
            PhoneNumber = string.Empty,
            Subject = new List<Subject>
            {
                new Subject { Name = "Math", Description = "Mathematics subject" },
                new Subject { Name = "Science", Description = "Science subject" },
                new Subject { Name = "English", Description = "English subject" }
            }
        };
        // var subjects = await dbContext.Subject.ToListAsync();
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddStudentsModel model)
    {

        var student = new Student
        {
            Name = model.Name,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            IsActive = model.IsActive,

        };
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
        return RedirectToAction("List", "Students");
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Fetch all students from the database
        var students = await dbContext.Students.ToListAsync();
        // Return the Index view with the list of students
        return View(students);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        // Find the student by ID
        var student = await dbContext.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        // Return the Edit view with the student data
        return View(student);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Student model)
    {
        // Find the student by ID
        var student = await dbContext.Students.FindAsync(model.Id);
        if (student is not null)
        {
            // Update the student's properties
            student.Name = model.Name;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;
            student.IsActive = model.IsActive;

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }
        // Redirect to the List action to show updated list
        return RedirectToAction("List", "Students");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        // Find the student by ID
        var student = await dbContext.Students.FindAsync(id);
        if (student is not null)
        {
            // Remove the student from the database
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }
        // Redirect to the List action to show updated list
        return RedirectToAction("List", "Students");
    }
}
