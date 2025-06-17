using DepartmentPortal.Models.Entities;
using DepartmentPortal.DTOs;
using AutoMapper;
using DepartmentPortal.Interfaces;

namespace DepartmentPortal.Services
{
    public class ServiceDepartment : IServiceDepartment
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public ServiceDepartment(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> AddAsync(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);
            department.Id = Guid.NewGuid();

            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);   
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
            
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto?> UpdateAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            var updatedDepartment = await _departmentRepository.UpdateAsync(department);
            
            if (updatedDepartment == null)
            {
                return null;
            }

            await _departmentRepository.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(updatedDepartment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _departmentRepository.DeleteAsync(id);
            if (result)
            {
                await _departmentRepository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _departmentRepository.ExistsAsync(id);
        }
    }
}