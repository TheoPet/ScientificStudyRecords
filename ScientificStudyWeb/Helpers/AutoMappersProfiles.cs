using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System.Linq;

namespace ScientificStudyWeb.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            CreateMap<Study, StudyData>()
            .ForMember(dest => dest.Tasks,
            opt => opt.MapFrom(src => src.Tasks.Select(task => task.Name)))
            .ForMember(dest => dest.Groups,
            opt => opt.MapFrom(src => src.StudyGroups.Select(group => group.Group.Name)))
            .ReverseMap()
            .ForMember(dest => dest.Tasks,
            opt => opt.MapFrom<StudyTaskResolver>())
            .ForMember(dest => dest.StudyGroups,
            opt => opt.MapFrom<StudyGroupResolver>());

            CreateMap<Task, TaskData>();

            CreateMap<TestSubjectData, TestSubject>()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study))
            .ForPath(dest => dest.Study.Id,
            opt => opt.MapFrom(src => src.StudyId))
            .ForPath(dest => dest.Group.Name,
            opt => opt.MapFrom(src => src.Group))
            .ForPath(dest => dest.GroupId,
            opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.Experiments,
            opt => opt.MapFrom<TestSubjectExperimentResolver>())
            .ReverseMap()
            .ForMember(dest => dest.Study,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForMember(dest => dest.Group,
            opt => opt.MapFrom(dest => dest.Group.Name));
        }
    }
}