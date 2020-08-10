using System.Collections.Generic;
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
            .Where(t => t.Id == Id)
            .Include(s => s.Study)
            .Include(g => g.Group)
            .Include(t => t.Experiments)
                .ThenInclude(e => e.Task)
            .FirstOrDefaultAsync();
        }

        public async Task<TestSubject> GetWithFilteredExperiments(int id, int groupId)
        {
          var testSubject =  await _scientificStudiesContext.TestSubjects
            .Where(t => t.Id == id)
            .Include(s => s.Study)
            .Include(g => g.Group)
            .Include(t => t.Experiments)
                .ThenInclude(e => e.Task)
            .FirstOrDefaultAsync();

            var filteredExperiments = testSubject.Experiments.Where(e => e.GroupId == groupId).ToList();
            testSubject.Experiments = filteredExperiments;
            return testSubject;
        }

        public new async Task<IEnumerable<TestSubject>> GetAll()
        {
            return await _scientificStudiesContext.TestSubjects
            .Include(s => s.Study).ToListAsync();
        }
    }
}
