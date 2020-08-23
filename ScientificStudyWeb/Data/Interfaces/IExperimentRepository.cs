using System.Threading.Tasks;
using ScientificStudyWeb.Helpers;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IExperimentRepository : IRepository<Experiment>
    {
        public Task<PagedList<Experiment>> GetAllFilteredByStudy(PaginationParameters parameters, int studyId);
        public Task<PagedList<Experiment>> GetAllFilteredByGroup(PaginationParameters parameters, int groupId);
        public Task<PagedList<Experiment>> GetAllFilteredByTestSubject(PaginationParameters parameters, int testSubjectId);
    }
}