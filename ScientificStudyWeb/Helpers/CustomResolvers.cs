using System.Collections.Generic;
using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

public class StudyBasicTaskResolver : IValueResolver<Study, StudyData, ICollection<BasicTaskData>>
{
    public ICollection<BasicTaskData> Resolve(Study source, StudyData destination, ICollection<BasicTaskData> destMember, ResolutionContext context)
    {
        var destinationTasks = new List<BasicTaskData>();
        if (source.Tasks == null)
            return destinationTasks;

        foreach (var task in source.Tasks)
            destinationTasks.Add(new BasicTaskData()
            {
                Name = task.Name,
                Id = task.Id
            });

        return destinationTasks;
    }
}

public class StudyBasicGroupResolver : IValueResolver<Study, StudyData, ICollection<BasicGroupData>>
{
    public ICollection<BasicGroupData> Resolve(Study source, StudyData destination, ICollection<BasicGroupData> destMember, ResolutionContext context)
    {
        var destinationGroups = new List<BasicGroupData>();
        if (source.Tasks == null)
            return destinationGroups;

        foreach (var group in source.StudyGroups)
        {
            destinationGroups.Add( new BasicGroupData(){
                Name = group.Group.Name,
                Id = group.GroupId 
            });
        }

        return destinationGroups;
    }
}

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

            var studyGroupToAdd = new StudyGroup();

            if (group.Id != null)
            {
                groupToAdd.Id = group.Id.Value;
                studyGroupToAdd.GroupId = group.Id.Value;
            }

            studyGroupToAdd.Group = groupToAdd;

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

public class TaskExperimentResolver : IValueResolver<TaskData, Task, ICollection<Experiment>>
{
    public ICollection<Experiment> Resolve(TaskData source, Task destination, ICollection<Experiment> destMember, ResolutionContext context)
    {
        var destExperiments = new List<Experiment>();

        foreach(var experiment in source.Experiments)
        {
            var experimentToAdd = new Experiment()
            {
                Time = experiment.Time,
                Comment = experiment.Comment,
                TestSubjectId = experiment.TestSubjectId,
                TaskId = experiment.TaskId
            };

            if (experiment.Id != null)
                experimentToAdd.Id = experiment.Id.Value;
            
            destExperiments.Add(experimentToAdd);
        }
        return destExperiments;
    } 
}