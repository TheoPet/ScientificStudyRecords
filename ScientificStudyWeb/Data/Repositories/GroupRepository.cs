using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Helpers;
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

        public new async Task<IEnumerable<Group>> GetAll()
        {
            return await _scientificStudiesContext.Groups
            .Include(g => g.Study).ToListAsync();
        }

        public async Task<PagedList<Group>> GetAllFiltered(SearchParameters parameters)
        {
            var groups = _scientificStudiesContext.Groups
            .Where(g => g.Name.ToLower().Contains(parameters.SearchTerm.ToLower()));
            return await PagedList<Group>.ToPagedListAsync(groups, parameters.PageNumber, parameters.PageSize);
        }
    }
}