using System.Collections.Generic;
using System.Threading.Tasks;
using ScientificStudyWeb.Helpers;
using ScientificStudyWeb.Models;
namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        public Task<PagedList<Group>> GetAllFiltered(SearchParameters parameters);

    }
}