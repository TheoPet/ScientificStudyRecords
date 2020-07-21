using System.Collections.Generic;
using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

public class StudyBasicTaskResolver : IValueResolver<Study, StudyData, ICollection<BasicData>>
{
    public ICollection<BasicData> Resolve(Study source, StudyData destination, ICollection<BasicData> destMember, ResolutionContext context)
    {
        var destinationTasks = new List<BasicData>();
        if (source.Tasks == null)
            return destinationTasks;

        foreach (var task in source.Tasks)
            destinationTasks.Add(new BasicData()
            {
                Name = task.Name,
                Id = task.Id
            });

        return destinationTasks;
    }
}

public class StudyBasicGroupResolver : IValueResolver<Study, StudyData, ICollection<BasicData>>
{
    public ICollection<BasicData> Resolve(Study source, StudyData destination, ICollection<BasicData> destMember, ResolutionContext context)
    {
        var destinationGroups = new List<BasicData>();
        if (source.Groups == null)
            return destinationGroups;

        foreach (var group in source.Groups)
        {
            destinationGroups.Add( new BasicData(){
                Name = group.Name,
                Id = group.Id
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
public class StudyGroupResolver : IValueResolver<StudyData, Study, ICollection<Group>>
{
    public ICollection<Group> Resolve(StudyData source, Study destination, ICollection<Group> destMember, ResolutionContext context)
    {
        var destGroups = new List<Group>();

        if (source.Groups == null)
            return destGroups;

          foreach (var group in source.Groups)
        {
            var newGroup = new Group()
            {
                Name = group.Name
            };

            if (group.Id != null)
                newGroup.Id = group.Id.Value;

            destGroups.Add(newGroup);

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

public class GroupTestSubjectResolver : IValueResolver<Group, GroupData, ICollection<BasicTestSubject>>
{
    public ICollection<BasicTestSubject> Resolve(Group source, GroupData destination, ICollection<BasicTestSubject> destMember, ResolutionContext context)
    {
        var destinationTestSubjects = new List<BasicTestSubject>();

        if (source.TestSubjects == null)
            return destinationTestSubjects;

        foreach(var subject in source.TestSubjects)
        {
            destinationTestSubjects.Add( new BasicTestSubject() {
                Name = subject.Name,
                Surname = subject.Surname,
                StudyId = subject.StudyId,
                Id = subject.Id
            });
        }

        return destinationTestSubjects;    
    }
}