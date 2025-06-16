using AutoMapper;
using DepartmentPortal.DTOs;
using DepartmentPortal.Models.Entities;

namespace DepartmentPortal.MappingProfiles;

public class MappingDepartment : Profile
{
    public MappingDepartment()
    {
        // DepartmentDto ⇄ DepartmentCreateDto
        CreateMap<DepartmentDto, DepartmentCreateDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<DepartmentCreateDto, DepartmentDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        // Department ⇄ DepartmentDto
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<DepartmentDto, Department>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        // DepartmentCreateDto → Department
        CreateMap<DepartmentCreateDto, Department>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}
