using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScientificStudyWeb.Data
{
    public class ExperimentRepository : Repository<Experiment>, IExperimentRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public ExperimentRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }
    }
}