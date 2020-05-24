using System.Collections.Generic;
using AutoMapper;
using ScientificStudiesRecord.DataObjects;
using ScientificStudyWeb.Models;

public class StudyTaskResolver : IValueResolver<StudyData, Study, ICollection<Task>>
{
    public ICollection<Task> Resolve(StudyData source, Study destination, ICollection<Task> destMember, ResolutionContext context)
    {
        var destTasks = new List<Task>();
        
        if(source.Tasks == null)
            return destTasks;
        
        foreach(var task in source.Tasks)
        {
            destTasks.Add(new Task(){Name = task});
        }
        return destTasks;
    }
}
public class StudyGroupResolver : IValueResolver<StudyData, Study, ICollection<StudyGroup>>
{
    public ICollection<StudyGroup> Resolve(StudyData source, Study destination, ICollection<StudyGroup> destMember, ResolutionContext context)
    {
        var destGroups = new List<StudyGroup>();
        
        if(source.Groups == null)
            return destGroups;

        foreach (var group in source.Groups)
        {
            destGroups.Add(new StudyGroup()
            {
                Group = new Group()
                { Name = group }
            });
        }
        return destGroups;
    }
}