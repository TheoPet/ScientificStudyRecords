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
        }
    }
}