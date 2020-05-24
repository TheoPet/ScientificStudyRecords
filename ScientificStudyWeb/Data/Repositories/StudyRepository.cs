using System.Threading.Tasks;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

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
            .Include(s=> s.Tasks)
            .Include(s=> s.StudyGroups)
            .ThenInclude( g=> g.Group)
            .Include(s=>s.TestSubjects).FirstOrDefaultAsync();           
        }

        public override  bool Remove (int Id)
        {
            var studyToDelete = _scientificStudiesContext.Studies
            .Where(s => s.Id == Id)
            .Include(s => s.Tasks)
            .Include(s => s.StudyGroups)
            .ThenInclude(g => g.Group)
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
    }
}