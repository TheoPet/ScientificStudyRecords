using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class TestSubjectRepository : Repository<TestSubject>, ITestSubjectRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public TestSubjectRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }

         public override async Task<TestSubject> Get(int Id)
        {
            return await _scientificStudiesContext.TestSubjects
            .Where(s => s.Id == Id)
            .Include(s => s.Study)
            .Include(g => g.Group).FirstOrDefaultAsync();           
        }
    }
}