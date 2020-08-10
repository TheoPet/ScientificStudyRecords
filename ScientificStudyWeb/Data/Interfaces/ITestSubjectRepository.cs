using System.Threading.Tasks;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface ITestSubjectRepository : IRepository<TestSubject>
    {
         public Task<TestSubject> GetWithFilteredExperiments(int id, int groupId);
    }
}