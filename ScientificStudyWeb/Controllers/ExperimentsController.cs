using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Save(ExperimentData experiment)
        {
            var experimentToSave = _mapper.Map<Experiment>(experiment);
            experimentToSave.Task = null;

            _unitOfWork.experimentRepository.Add(experimentToSave);
            await _unitOfWork.SaveChangesAsync();

            var experimentToReturn = _mapper.Map<ExperimentData>(experimentToSave);
            experimentToReturn.Task.Id = experiment.Task.Id;
            experimentToReturn.Task.Name = experiment.Task.Name;

            return Ok(experimentToReturn);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ExperimentData experiment)
        {
            var experimentToUpdate = await _unitOfWork.experimentRepository.Get(experiment.Id.Value);
            experimentToUpdate.Time = experiment.Time;
            experimentToUpdate.Comment = experiment.Comment;

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExperiment(int id)
        {
            var experiment = await _unitOfWork.experimentRepository.Get(id);
            var experimentToReturn = _mapper.Map<ExperimentData>(experiment);

            return Ok(experimentToReturn);
        }
    }
}