using System.Collections.Generic;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IStudyRepository : IRepository<Study>
    {
        void UpdateTasks(ICollection<Task> modelTasks, ICollection<Task> clientTasks);
        void UpdateStudyGroups(ICollection<StudyGroup> modelGroups, ICollection<StudyGroup> clientGroups);
         
    }
}