using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;

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

        [HttpPost("savetestsubject")]
        public async Task<IActionResult> SaveTestSubject(TestSubjectData data)
        {
            //studija i grupa moraju postojati u bazi!
            await unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}