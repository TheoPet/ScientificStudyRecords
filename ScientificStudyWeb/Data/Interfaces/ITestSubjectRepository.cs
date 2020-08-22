using System.Threading.Tasks;
using ScientificStudyWeb.Helpers;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface ITestSubjectRepository : IRepository<TestSubject>
    {
        public Task<TestSubject> GetWithFilteredExperiments(int id, int groupId);

        public Task<PagedList<TestSubject>> GetAllFiltered(SearchParameters parameters);

    }
}