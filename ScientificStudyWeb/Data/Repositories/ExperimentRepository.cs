using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ScientificStudyWeb.Data
{
    public class ExperimentRepository : Repository<Experiment>, IExperimentRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public ExperimentRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }

        public override async Task<Experiment> Get(int id) {
            return await _scientificStudiesContext.Experiments
            .Where(e => e.Id == id)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .FirstOrDefaultAsync();
        }
    }
}