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
    }
}