using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using DepartmentPortal.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DepartmentPortal.Services;

public class ServiceDepartment 
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceDepartment(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<DepartmentDto> AddAsync(DepartmentCreateDto departmentCreateDto)
    {
        var department = _mapper.Map<Department>(departmentCreateDto);
        department.Id = Guid.NewGuid();
        await _dbContext.Departments.AddAsync(department);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _dbContext.Departments.ToListAsync();
        return _mapper.Map<List<DepartmentDto>>(departments);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = await _dbContext.Departments.FindAsync(id);
        if (department == null) return false;

        _dbContext.Departments.Remove(department);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}