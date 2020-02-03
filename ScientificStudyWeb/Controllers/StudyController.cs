using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using System.Collections.Generic;
using ScientificStudiesRecord.DataObjects;

namespace ScientificStudyWeb.Controllers
{
    [Route("master/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork unitOfWork;

        public StudyController(ScientificStudiesRecordDbContext context)
        {
            _context = context;
            this.unitOfWork = new UnitOfWork(_context); 
        }


       /* [HttpGet("studies")]
        public async Task<IActionResult> GetStudies()
        {
            var studies = await _context.Studies.ToListAsync();
            return Ok(studies);
        }*/

        [HttpPost("test")]
        public async Task<IActionResult> TestCode()
        {
            var study = new Study
            {
                Name = "std"
            };
            unitOfWork.studyRepository.Add(study);

            var group = new Group { Name = "groupName" };
            var group2 = new Group {Name = "groupName2"};
            
            unitOfWork.groupRepository.Add(group);
            unitOfWork.groupRepository.Add(group2);
            
            study.StudyGroups = new List<StudyGroup>
            {
                new StudyGroup {
                    Study = study,
                    Group = group
                    }
            };
            study.StudyGroups = new List<StudyGroup>
            {
                new StudyGroup {
                    Study = study,
                    Group = group2
                    }
            };

            unitOfWork.taskRepository.Add
            (
                new ScientificStudyWeb.Models.Task
                {
                    Name = "taskName"
                }
                );
           await unitOfWork.SaveChangesAsync();
           return Ok();
        }

        [HttpPost("savestudies")]
        public async Task<IActionResult> SaveStudies(StudyData data)

        {
            var study = new Study
            {
                Name = data.Study
            };

            unitOfWork.studyRepository.Add(study);        
            study.StudyGroups = new List<StudyGroup>();
            foreach (var groupName in data.Groups)
            {
                var group = new Group { Name = groupName };

                study.StudyGroups.Add(
                  new StudyGroup
                  {
                      Study = study,
                      Group = group
                  }
                );

                unitOfWork.groupRepository.Add(group);
            }

            foreach(var taskName in data.Tasks)
            {
                var task = new ScientificStudyWeb.Models.Task
                {
                    Name = taskName,
                    Study = study
                };
                unitOfWork.taskRepository.Add(task);
                
            }

            await unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}