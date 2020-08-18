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
            destinationTasks.Add(new BasicData(task.Name, task.Id));

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
            destinationGroups.Add(new BasicData(group.Name, group.Id));


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

        if (source.Experiments == null)
            return destExperiments;

        foreach (var experiment in source.Experiments)
        {
            var experimentToAdd = new Experiment()
            {
                Time = experiment.Time,
                Comment = experiment.Comment,
                TestSubjectId = experiment.TestSubject.Id.Value,
                GroupId = experiment.GroupId,
                TaskId = experiment.Task.Id.Value
            };

            if (experiment.Id != null)
                experimentToAdd.Id = experiment.Id.Value;

            destExperiments.Add(experimentToAdd);
        }
        return destExperiments;
    }
}
public class TestSubjectDataExperimentResolver : IValueResolver<TestSubject, TestSubjectData, ICollection<ExperimentData>>

{
    public ICollection<ExperimentData> Resolve(TestSubject source, TestSubjectData destination, ICollection<ExperimentData> destMember, ResolutionContext context)
    {
        var destExperiments = new List<ExperimentData>();

        if (source.Experiments == null)
            return destExperiments;

        foreach (var experiment in source.Experiments)
        {
            var experimentToAdd = new ExperimentData()
            {
                Id = experiment.Id,
                Time = experiment.Time,
                Comment = experiment.Comment,
                TestSubject = new BasicTestSubject(experiment.TestSubject.Name,
                experiment.TestSubject.Surname,
                experiment.TestSubject.Id),
                GroupId = experiment.GroupId,
                Task = new BasicData(experiment.Task.Name, experiment.TaskId)
            };

            destExperiments.Add(experimentToAdd);
        }
        return destExperiments;
    }
}
public class TaskDataExperimentResolver : IValueResolver<TaskData, Task, ICollection<Experiment>>
{
    public ICollection<Experiment> Resolve(TaskData source, Task destination, ICollection<Experiment> destMember, ResolutionContext context)
    {
        var destExperiments = new List<Experiment>();
        if (source.Experiments == null)
            return destExperiments;

        foreach (var experiment in source.Experiments)
        {
            var experimentToAdd = new Experiment()
            {
                Time = experiment.Time,
                Comment = experiment.Comment,
                TestSubjectId = experiment.TestSubject.Id.Value,
                GroupId = experiment.GroupId,
                TaskId = experiment.Task.Id.Value
            };

            if (experiment.Id != null)
                experimentToAdd.Id = experiment.Id.Value;

            destExperiments.Add(experimentToAdd);
        }
        return destExperiments;
    }
}

public class TaskExperimentResolver : IValueResolver<Task, TaskData, ICollection<ExperimentData>>
{
    public ICollection<ExperimentData> Resolve(Task source, TaskData destination, ICollection<ExperimentData> destMember, ResolutionContext context)
    {
        var destExperiments = new List<ExperimentData>();
        if (source.Experiments == null)
            return destExperiments;

        foreach (var experiment in source.Experiments)
        {
            var experimentToAdd = new ExperimentData()
            {
                Time = experiment.Time,
                Comment = experiment.Comment,
                TestSubject = new BasicTestSubject(experiment.TestSubject.Name,
                experiment.TestSubject.Surname, experiment.TestSubjectId),
                GroupId = experiment.GroupId,
                Task = new BasicData(experiment.Task.Name, experiment.Task.Id)
            };

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

        foreach (var subject in source.TestSubjects)
        {
            destinationTestSubjects.Add(new BasicTestSubject(subject.Name,subject.Surname,subject.Id));
        }

        return destinationTestSubjects;
    }
}