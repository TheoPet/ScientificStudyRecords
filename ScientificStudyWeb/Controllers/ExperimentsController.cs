using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExperimentsController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public ExperimentsController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IEnumerable<ExperimentData> data)
        {
            var dataToSave = _mapper.Map<IEnumerable<Experiment>>(data);
            _unitOfWork.experimentRepository.AddRange(dataToSave);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{taskId:int}")]
        public async Task<IActionResult> GetExperimentsLookup(int taskId)
        {
            var experiments = await _unitOfWork.experimentRepository.GetAll(e => e.TaskId.Equals(taskId));
            var experimentsToReturn = _mapper.Map<IEnumerable<ExperimentData>>(experiments);

            return Ok (experimentsToReturn);
        }
    } 
}