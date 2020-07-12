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
            .ForMember( dest => dest.Tasks,
            opt => opt.MapFrom<StudyBasicTaskResolver>())
            .ForMember(dest => dest.Groups,
            opt => opt.MapFrom<StudyBasicGroupResolver>())
            .ReverseMap()
            .ForMember(dest => dest.Tasks,
            opt => opt.MapFrom<StudyTaskResolver>())
            .ForMember(dest => dest.StudyGroups,
            opt => opt.MapFrom<StudyGroupResolver>());

            CreateMap<TestSubjectData, TestSubject>()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForPath(dest => dest.Study.Id,
            opt => opt.MapFrom(src => src.Study.Id))
            .ForPath(dest => dest.Group.Name,
            opt => opt.MapFrom(src => src.Group.Name))
            .ForPath(dest=> dest.Group.Id,
            opt => opt.MapFrom(src => src.Group.Id))
            .ForPath(dest => dest.GroupId,
            opt => opt.MapFrom(src => src.Group.Id))
            .ForMember(dest => dest.EntryTime,
            opt => opt.MapFrom(src => src.EntryTime))
            .ForMember(dest => dest.Experiments,
            opt => opt.MapFrom<TestSubjectExperimentResolver>())
            .ReverseMap()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForPath(dest => dest.Group.Name,
            opt => opt.MapFrom(src => src.Group.Name))
            .ForPath(dest => dest.Group.Id,
            opt => opt.MapFrom(src => src.GroupId));

            CreateMap<Study, BasicStudyData>();

            CreateMap<StudyGroup, BasicGroupData>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Group.Name))
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => src.GroupId));

            CreateMap<TestSubject, BasicTestSubject>()
            .ForPath(dest => dest.StudyId,
            opt => opt.MapFrom(src => src.StudyId.Value));


            CreateMap<ExperimentData, Experiment>()
            .ReverseMap();

            CreateMap<TaskData, Task>()
            .ForMember(dest => dest.Study,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForMember(dest => dest.StudyId,
            opt => opt.MapFrom(src => src.Study.Id))
            .ForMember(dest => dest.Experiments,
            opt => opt.MapFrom<TaskExperimentResolver>());

            CreateMap<Task, BasicTaskData>();
        }
    }
}