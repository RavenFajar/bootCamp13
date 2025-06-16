using AutoMapper;
using DepartmentPortal.Models.Entities;
using DepartmentPortal.DTOs;

namespace DepartmentPortal.MappingProfiles;

public class MappingDepartment : Profile
{
    public MappingDepartment()
    {
        CreateMap<DepartmentDto, DepartmentCreateDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<DepartmentCreateDto, DepartmentDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}
