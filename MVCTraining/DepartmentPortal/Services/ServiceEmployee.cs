using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using DepartmentPortal.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DepartmentPortal.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepartmentPortal.Services;

public class ServiceEmployee : IServiceEmployee
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceEmployee(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> AddAsync(EmployeeCreateDto employeeCreateDto)
    {
    var employee = _mapper.Map<Employee>(employeeCreateDto);

    // Cari department yang sudah ada
    employee.Department = await _dbContext.Departments.FindAsync(employeeCreateDto.DepartmentId);

    await _dbContext.Employees.AddAsync(employee);
    await _dbContext.SaveChangesAsync();

    return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        var Employees = await _dbContext.Employees.ToListAsync();
        return _mapper.Map<List<EmployeeDto>>(Employees);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var Employee = await _dbContext.Employees.FindAsync(id);
        if (Employee == null) return false;

        _dbContext.Employees.Remove(Employee);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<List<SelectListItem>> GetDepartmentsAsSelectListAsync()
    {
    return await _dbContext.Departments
        .Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name
        }).ToListAsync();
    }
}