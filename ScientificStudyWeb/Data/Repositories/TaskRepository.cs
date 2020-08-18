using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class TaskRepository : Repository<Models.Task>, ITaskRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public TaskRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }

        public override async Task<Models.Task> Get(int Id)
        {
            return await _scientificStudiesContext.Tasks
            .Where(t => t.Id == Id)
            .Include(t => t.Study)
            .Include(t => t.Experiments)
            .ThenInclude(e => e.TestSubject).FirstOrDefaultAsync();

        }

    }
}