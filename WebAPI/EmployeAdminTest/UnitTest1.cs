using EmployeeAdmin.Controllers;
using EmployeeAdmin.Data;
using EmployeeAdmin.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;

namespace EmployeeAdminTest;

[TestFixture]
public class EmployeesControllerTests
{
    // Mocking the DbSet and DbContext for testing
    // Using Moq to create a mock of the DbSet<Employee> and ApplicationtDBContext
    private Mock<DbSet<Employee>> mockSet;
    private Mock<ApplicationtDBContext> dbContextMock;
    private EmployeesController controller;

    [SetUp]
    public void Setup()
    {
        var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), Name = "John Doe", Email = "testing@gmail.com", Salary = 50000 },
                new Employee { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "testing2@gmail.com", Salary = 60000 }
            }.AsQueryable();
        dbContextMock = new Mock<ApplicationtDBContext>();
        mockSet = new Mock<DbSet<Employee>>();
        mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
        mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
        mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
        mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
        dbContextMock.Setup(m => m.Employees).Returns(mockSet.Object);


    }

    [Test]
    public void GetAllEmployees_ReturnsOkWithAllEmployees()
    {
        // Act
        var result = dbContextMock.Object.Employees.ToList();

        // Assert
        Assert.Equals(result, Is.InstanceOf<OkObjectResult>());
    }
}
