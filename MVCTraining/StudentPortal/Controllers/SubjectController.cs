using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models.Entities;
using StudentPortal.Models;
using StudentPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentPortal.Controllers;

public class SubjectController : Controller
{
    private readonly ApplicationDBContext dbContext;

    public SubjectController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }
   [HttpGet]
   public IActionResult Add()
   {
      // This action method will return the Index view for the Students controller.
      return View();
   }
   [HttpPost]
   public  async Task<IActionResult> Add(AddSubjectModel model)
   {
        var subject = new Subject
        {
            Name = model.Name,
            Description = model.Description,
            IsActive = model.IsActive,
        };
        await dbContext.Subject.AddAsync(subject);
        await dbContext.SaveChangesAsync();
       return RedirectToAction("List", "Subject");
   }
   [HttpGet]
    public async Task<IActionResult> List()
    {
         // Fetch all students from the database
         var subjects = await dbContext.Subject.ToListAsync();
         // Return the Index view with the list of students
         return View(subjects);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        // Find the student by ID
        var subject = await dbContext.Subject.FindAsync(id);
        if (subject == null)
        {
            return NotFound();
        }
        // Return the Edit view with the student data
        return View(subject);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Subject model)
    {
        // Find the student by ID
        var subject = await dbContext.Subject.FindAsync(model.Id);
        if (subject is not null)
        {
            // Update the student's properties
            subject.Name = model.Name;
            subject.Description = model.Description;
            subject.IsActive = model.IsActive;

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }else{
            return NotFound();
        }
        // Redirect to the List action to show updated list
        return RedirectToAction("List", "Subject");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        // Find the student by ID
        var subject = await dbContext.Subject.FindAsync(id);
        if (subject is not null)
        {
            // Remove the student from the database
            dbContext.Subject.Remove(subject);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }
        // Redirect to the List action to show updated list
        return RedirectToAction("List", "Subject");
    }
}
