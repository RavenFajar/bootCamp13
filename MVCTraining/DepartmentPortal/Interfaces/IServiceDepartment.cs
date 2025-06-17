using System;
using DepartmentPortal.DTOs;
namespace DepartmentPortal.Interfaces;


public interface IServiceDepartment
{
    Task<DepartmentDto> AddAsync(DepartmentCreateDto departmentCreateDto);
    Task<List<DepartmentDto>> GetAllAsync();
    Task<bool> DeleteAsync(Guid id);

    Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
    Task<DepartmentDto?> UpdateAsync(DepartmentDto departmentDto);
    Task<bool> ExistsAsync(Guid id);

}