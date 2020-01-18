using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data;

namespace ScientificStudyWeb.Controllers
{
    [Route("master/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        public StudyController(ScientificStudiesRecordDbContext context)
        {
            _context = context;
        }


        [HttpGet("studies")]
        public async Task<IActionResult> GetStudies()
        {
            var studies = await _context.Studies.ToListAsync();
            return Ok(studies);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            var groups =  await _context.Groups.ToListAsync();
            return Ok(groups);
        }
        
        [HttpGet("deletestudies")]
        public async Task<IActionResult>DeleteStudies()
        {
            var study = await _context.Studies.FirstOrDefaultAsync();
          /*  var task = new ScientificStudiesRecord.Models.Task
            {
                Name = "Test2",
                Study =study
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            study = await _context.Studies.FirstOrDefaultAsync();
            _context.Studies.Remove(study);*/
            await _context.SaveChangesAsync();
             return Ok();

        }
    }
}