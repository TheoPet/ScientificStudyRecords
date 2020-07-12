using System.Threading.Tasks;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            .ThenInclude( g => g.Group)
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

        public void UpdateTasks(ICollection<Models.Task> modelTasks, ICollection<Models.Task> clientTasks)
        {
            var tasksToUpdate = from updateTask in clientTasks
                       where updateTask.Id > 0
                       select updateTask;

            foreach (var task in tasksToUpdate)
                modelTasks.Where(t => t.Id.Equals(task.Id)).Single().Name = task.Name;

            var tasksToAdd = from taskToAdd in clientTasks
                             where taskToAdd.Id == 0
                             select taskToAdd;

            foreach (var task in tasksToAdd)
                modelTasks.Add(task);
        }

        public void UpdateStudyGroups(ICollection<StudyGroup> modelGroups, ICollection<StudyGroup> clientGroups)
        {
            var groupsToUpdate = from updateGroup in clientGroups
                                 where updateGroup.GroupId > 0
                                 select updateGroup;
            
            foreach (var groupToUpdate in groupsToUpdate)
                modelGroups.Where(g => g.GroupId.Equals(groupToUpdate.GroupId)).Single().Group.Name = groupToUpdate.Group.Name;

            var groupsToAdd = from groupToAdd in clientGroups
                              where groupToAdd.GroupId == 0
                              select groupToAdd;

            foreach(var groupToAdd in groupsToAdd)
                modelGroups.Add(groupToAdd);

        }
    }
}