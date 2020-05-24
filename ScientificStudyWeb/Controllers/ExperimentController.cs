using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Controllers
{
    [Route("master/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork unitOfWork;

        public ExperimentController(ScientificStudiesRecordDbContext context)
        {
            _context = context;
            this.unitOfWork = new UnitOfWork(_context); 
        }

        [HttpGet("gettestsubject")]
        public async Task<IActionResult> GetTestSubject(string testSubjectName)
        {
            //TO DO: sta kada je ovo prazno? 
            //To Do: pravi search, ovo je samo najjednostavnija verzija
            var testSubject = await unitOfWork.testSubjectRepository.Get(t => t.Name.Equals(testSubjectName));
            // TO DO: Ovde treba da ide mapiranje u DTO
            // I hesiranje??? Da li da se odmah vrate taskovi za studiju?
            return Ok(testSubject);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(ExperimentData data)
        {
            var study = await unitOfWork.studyRepository.Get(s => s.Name.Equals(data.Study));
            var task = study.Tasks.FirstOrDefault(t =>t.Equals(data.Task));
            //treba da rastavis testSubject na name i surname i onda da ga nadjes u abzi??
            var experiment = new Experiment
            {
                //OVDE TI FALI KONVERZIJA IZ HH:MM U NE ZNAM NI JA STA, SEKUNDE???
                Duration = Convert.ToInt64(data.Duration),
                Comment = data.Comment,
                Task = task,
            };
            return Ok();
        }
    }
}