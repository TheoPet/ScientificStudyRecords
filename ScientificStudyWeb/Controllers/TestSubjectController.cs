using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System.Linq;

namespace ScientificStudyWeb.Controllers
{
    [Route("master/[controller]")]
    [ApiController]

    public class TestSubjectController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork unitOfWork;

        public TestSubjectController(ScientificStudiesRecordDbContext context)
        {
            _context = context;
            this.unitOfWork = new UnitOfWork(_context); 
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveTestSubject(TestSubjectData data)
        {
            //studija i grupa moraju postojati u bazi!
            var study = await unitOfWork.studyRepository.Get(s => s.Name.Equals(data.Study));
            var group = await unitOfWork.groupRepository.Get(g => g.Name.Equals(data.Group));

            var testSubject = new TestSubject
            {
                Name = data.Name,
                Surname = data.Surname,
                EntryTime = DateTime.Now,
                Comment = data.Comment,
                Study = study,
                Group = group
            };

            unitOfWork.testSubjectRepository.Add(testSubject);
            await unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}