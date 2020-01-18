using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public TaskRepository(ScientificStudiesRecordDbContext context) : base (context)
        {
            _scientificStudiesContext = context;
        }
    }
}