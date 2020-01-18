using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class StudyRepository : Repository<Study>, IStudyRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public StudyRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }
    }
}