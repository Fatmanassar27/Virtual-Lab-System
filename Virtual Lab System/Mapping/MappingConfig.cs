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

            CreateMap<Experiment, ExperimentDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.UserName))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            CreateMap<ExperimentDto, Experiment>().AfterMap((src, dest) =>
                {
                    dest.PdfFileName = src.PdfPath;
                    dest.TeacherId = src.TeacherId;
                    dest.SubjectId = src.SubjectId;
                    dest.Description = src.Description;
                    dest.Title = src.Title;
                }).ReverseMap();
            CreateMap<Report, ReportDto>().AfterMap((src, dest) =>
            {
                dest.Id = src.Id;
                dest.StudentName = src.Student.UserName;
                dest.Results = src.ResultData;
                dest.SubmissionDate = src.SubmissionDate;
                dest.ExperimentId = src.ExperimentId;
                dest.ExperimentTitle = src.Experiment.Title;
            }).ReverseMap();

            CreateMap<ApplicationUser, TeacherDto>()
               .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name)).ReverseMap();

            CreateMap<ApplicationUser, StudentDto>().ReverseMap();
        }
    }
}

