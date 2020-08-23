using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using ScientificStudyWeb.Helpers;

namespace ScientificStudyWeb.Data
{
    public class ExperimentRepository : Repository<Experiment>, IExperimentRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public ExperimentRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }

        public override async Task<Experiment> Get(int id)
        {
            return await _scientificStudiesContext.Experiments
            .Where(e => e.Id == id)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Experiment>> GetAll(Expression<Func<Experiment, bool>> predicate)
        {
            return await _scientificStudiesContext.Experiments
            .Where(predicate)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Group)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Study)
            .ToListAsync();
        }

        public async Task<PagedList<Experiment>> GetAllFilteredByGroup(PaginationParameters parameters, int groupId)
        {
            var experiments = _scientificStudiesContext.Experiments
            .Where(e => e.GroupId == groupId)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Group)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Study);
            return await PagedList<Experiment>.ToPagedListAsync(experiments, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Experiment>> GetAllFilteredByStudy(PaginationParameters parameters, int studyId)
        {
            var experiments = _scientificStudiesContext.Experiments
            .Where(e => e.StudyId == studyId)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Group)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Study);;
            return await PagedList<Experiment>.ToPagedListAsync(experiments, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Experiment>> GetAllFilteredByTestSubject(PaginationParameters parameters, int testSubjectId)
        {
            var experiments = _scientificStudiesContext.Experiments
            .Where(e => e.TestSubjectId == testSubjectId)
            .Include(e => e.Task)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Group)
            .Include(e => e.TestSubject)
            .ThenInclude(t => t.Study);;
            return await PagedList<Experiment>.ToPagedListAsync(experiments, parameters.PageNumber, parameters.PageSize);
        }
    }
}