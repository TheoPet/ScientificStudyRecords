using System.Threading.Tasks;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ScientificStudyWeb.Helpers;

namespace ScientificStudyWeb.Data
{
    public class StudyRepository : Repository<Study>, IStudyRepository
    {
        private readonly ScientificStudiesRecordDbContext _scientificStudiesContext;

        public StudyRepository(ScientificStudiesRecordDbContext context) : base(context)
        {
            _scientificStudiesContext = context;
        }

        public override async Task<Study> Get(int Id)
        {
            return await _scientificStudiesContext.Studies
            .Where(s => s.Id == Id)
            .Include(s => s.Tasks)
            .Include(s => s.Groups)
            .Include(s => s.TestSubjects).FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Study>> GetAll()
        {
            return await _scientificStudiesContext.Studies
            .Include(s => s.Groups)
            .Include(s => s.Tasks)
            .Include(s => s.TestSubjects).ToListAsync();
        }

        public override async Task<IEnumerable<Study>> GetAll(Expression<Func<Study, bool>> predicate)
        {
            return await _scientificStudiesContext.Studies
            .Where(predicate)
            .Include(s => s.Groups)
            .Include(s => s.Tasks)
            .Include(s => s.TestSubjects).ToListAsync();
        }
        public async Task<PagedList<Study>> GetAllFiltered(SearchParameters parameters)
        {
            var studies =  _scientificStudiesContext.Studies
            .Where(studies=> studies.Name.ToLower().Contains(parameters.SearchTerm.ToLower()));
            return await PagedList<Study>.ToPagedListAsync(studies, parameters.PageNumber, parameters.PageSize);
        }
        public override bool Remove(int Id)
        {
            var studyToDelete = _scientificStudiesContext.Studies
            .Where(s => s.Id == Id)
            .Include(s => s.Tasks)
            .Include(s => s.Groups)
            .Include(s => s.TestSubjects).FirstOrDefaultAsync();

            if (studyToDelete == null)
                return false;

            try
            {
                _scientificStudiesContext.Studies.Remove(studyToDelete.Result);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void AddGroup(string groupName, Study study)
        {
            study.Groups.Add(new Group() { Name = groupName });

        }

        public void AddTask(string name, Study study)
        {
            study.Tasks.Add(new Models.Task() { Name = name });
        }

        public void RemoveTask(int taskId, Study study)
        {
            var taskToDelete = study.Tasks.FirstOrDefault(t => t.Id.Equals(taskId));
            study.Tasks.Remove(taskToDelete);
        }

        public void RemoveGroup(int groupId, Study study)
        {
            var groupToDelete = study.Groups.FirstOrDefault(g => g.Id.Equals(groupId));
            study.Groups.Remove(groupToDelete);
        }
    }
}