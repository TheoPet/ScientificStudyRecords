using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<Group> Get(int Id)
        {
            return await _scientificStudiesContext.Groups
            .Where(g => g.Id == Id)
            .Include(g => g.TestSubjects).FirstOrDefaultAsync();
        }
        
    }
}