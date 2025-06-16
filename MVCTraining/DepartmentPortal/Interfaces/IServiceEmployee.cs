using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using DepartmentPortal.DTOs;

namespace DepartmentPortal.Interfaces;


public interface IServiceEmployee
{
    Task<EmployeeDto> AddAsync(EmployeeCreateDto employeeCreateDto);
    Task<List<EmployeeDto>> GetAllAsync();
    Task<bool> DeleteAsync(Guid id);
    Task<List<SelectListItem>> GetDepartmentsAsSelectListAsync();
}