using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;
namespace ScientificStudyWeb.Data
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public GroupRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }
        
    }
}