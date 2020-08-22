using System.Collections.Generic;
using System.Threading.Tasks;
using ScientificStudyWeb.Helpers;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IStudyRepository : IRepository<Study>
    {
        void AddTask(string Name, Study Study);
        void AddGroup(string name, Study study);

        void RemoveTask(int TaskId, Study study);
        void RemoveGroup(int GroupId, Study study);

        Task<PagedList<Study>> GetAllFiltered(SearchParameters parameters);



    }
}