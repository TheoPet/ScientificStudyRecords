using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System.Collections.Generic;
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
            .ForMember(dest => dest.Groups,
            opt => opt.MapFrom<StudyGroupResolver>());

            CreateMap<TestSubjectData, TestSubject>()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForPath(dest => dest.Study.Id,
            opt => opt.MapFrom(src => src.Study.Id.Value))
            .ForPath(dest => dest.Group.Name,
            opt => opt.MapFrom(src => src.Group.Name))
            .ForPath(dest=> dest.Group.Id,
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
            opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.Experiments,
            opt => opt.MapFrom<TestSubjectDataExperimentResolver>());

            CreateMap<Study, BasicData>();

            CreateMap<Group, BasicData>();
           

            CreateMap<TestSubject, BasicTestSubject>();
            
            CreateMap<ExperimentData, Experiment>()
            .ForPath(dest => dest.Task.Id,
            opt => opt.MapFrom(src => src.Task.Id))
            .ForPath(dest => dest.Task.Name,
            opt => opt.MapFrom(src => src.Task.Name))
            .ForPath( dest => dest.TestSubject.Name,
            opt => opt.MapFrom(src => src.TestSubject.Name))
            .ForPath( dest => dest.TestSubject.Surname,
            opt => opt.MapFrom(src => src.TestSubject.Surname))
            .ForPath( dest => dest.TestSubject.Id,
            opt => opt.MapFrom(src => src.TestSubject.Id))
            .ReverseMap()
            .ForPath(dest => dest.Task.Id,
            opt => opt.MapFrom(src => src.Task.Id))
            .ForPath(dest => dest.Task.Name,
            opt => opt.MapFrom(src => src.Task.Name));

            CreateMap<Experiment, ReportExperimentData>()
            .ForMember(dest => dest.Group,
            opt => opt.MapFrom(src => src.TestSubject.Group.Name))
            .ForMember(dest => dest.Study,
            opt => opt.MapFrom(src => src.TestSubject.Study.Name))
            .ForMember(dest => dest.Task,
            opt => opt.MapFrom(src => src.Task.Name))
            .ForMember(dest => dest.TestSubject,
            opt => opt.MapFrom(src => (src.TestSubject.Name + ' ' + src.TestSubject.Surname)))
            .ForMember(dest => dest.TestSubjectComment,
            opt => opt.MapFrom(src => src.TestSubject.Comment))
            .ForMember(dest => dest.TestSubjectEntryTime,
            opt => opt.MapFrom(src => src.TestSubject.EntryTime));

            CreateMap<TaskData, Task>()
            .ForMember(dest => dest.Study,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForMember(dest => dest.StudyId,
            opt => opt.MapFrom(src => src.Study.Id))
            .ForMember(dest => dest.Experiments,
            opt => opt.MapFrom<TaskDataExperimentResolver>())
            .ReverseMap()
            .ForPath( dest => dest.Study.Id,
            opt => opt.MapFrom( src => src.Study.Id))
              .ForPath( dest => dest.Study.Name,
            opt => opt.MapFrom( src => src.Study.Name))
            .ForMember(src => src.Experiments,
            opt => opt.MapFrom<TaskExperimentResolver>());

            CreateMap<Task, BasicData>();

            CreateMap<Group, GroupData>()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForPath(dest => dest.Study.Id,
            opt => opt.MapFrom(src => src.Study.Id))
            .ForMember( dest => dest.TestSubjects,
            opt => opt.MapFrom<GroupTestSubjectResolver>())
            .ReverseMap()
            .ForPath(dest => dest.Study.Name,
            opt => opt.MapFrom(src => src.Study.Name))
            .ForPath(dest => dest.Study.Id,
            opt => opt.MapFrom(src => src.Study.Id))
            .ForMember(dest => dest.StudyId,
            opt => opt.MapFrom(src => src.Study.Id));

        }
    }
}