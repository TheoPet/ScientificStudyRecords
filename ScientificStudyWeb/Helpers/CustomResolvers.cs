using System.Collections.Generic;
using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

public class StudyTaskResolver : IValueResolver<StudyData, Study, ICollection<Task>>
{
    public ICollection<Task> Resolve(StudyData source, Study destination, ICollection<Task> destMember, ResolutionContext context)
    {
        var destTasks = new List<Task>();

        if (source.Tasks == null)
            return destTasks;

        foreach (var task in source.Tasks)
        {
            var newTask = new Task()
            {
                Name = task.Name
            };

            if (task.Id != null)
                newTask.Id = task.Id.Value;

            destTasks.Add(newTask);

        }
        return destTasks;
    }
}
public class StudyGroupResolver : IValueResolver<StudyData, Study, ICollection<StudyGroup>>
{
    public ICollection<StudyGroup> Resolve(StudyData source, Study destination, ICollection<StudyGroup> destMember, ResolutionContext context)
    {
        var destGroups = new List<StudyGroup>();

        if (source.Groups == null)
            return destGroups;

        foreach (var group in source.Groups)
        {
            var groupToAdd = new Group()
            {
                Name = group.Name
            };

            if (group.Id != null)
                groupToAdd.Id = group.Id.Value;

            var studyGroupToAdd = new StudyGroup() { Group = groupToAdd };

            if (group.Id != null)
                studyGroupToAdd.GroupId = group.Id.Value;

            destGroups.Add(studyGroupToAdd);
        }
        return destGroups;
    }
}

public class TestSubjectExperimentResolver : IValueResolver<TestSubjectData, TestSubject, ICollection<Experiment>>

{
    public ICollection<Experiment> Resolve(TestSubjectData source, TestSubject destination, ICollection<Experiment> destMember, ResolutionContext context)
    {
        var destExperiments = new List<Experiment>();
        return destExperiments;
    }
}