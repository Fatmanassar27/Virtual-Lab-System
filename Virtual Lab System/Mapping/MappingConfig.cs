using AutoMapper;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Subject, SubjectDetailsDto>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers.Select(t => t.UserName)))
                .ForMember(dest => dest.Experiments, opt => opt.MapFrom(src => src.Experiments.Select(e => e.Title))).ReverseMap();
        }
    }
}
