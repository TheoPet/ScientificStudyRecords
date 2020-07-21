using System.Collections.Generic;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IStudyRepository : IRepository<Study>
    {
        void AddTask(string Name,Study Study);
        void AddGroup(string name, Study study);

        void RemoveTask(int TaskId, Study study);
        void RemoveGroup(int GroupId, Study study);
          
    }
}