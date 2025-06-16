using AutoMapper;
using DepartmentPortal.DTOs;
using DepartmentPortal.Models.Entities;

namespace DepartmentPortal.MappingProfiles;

public class MappingEmployee : Profile
{
    public MappingEmployee()
    {
        // Employee → EmployeDto
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

        // EmployeDto → Employee
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

        // EmployeCreateDto → Employee
        CreateMap<EmployeeCreateDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID biasanya di-generate oleh sistem
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            // .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => new Department { Id = src.DepartmentId }));

        // Employee → EmployeCreateDto (optional, jika kamu butuh pre-fill form edit misalnya)
        CreateMap<Employee, EmployeeCreateDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id));

    }
}
