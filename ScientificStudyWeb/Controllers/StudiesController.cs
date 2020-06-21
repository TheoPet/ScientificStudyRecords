using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using ScientificStudyWeb.DataObjects;
using System.Linq;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public StudiesController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(_context); 
        }

        [HttpGet]
        public async Task<IActionResult> GetStudies(bool simplified)
        {
            var studies = await _unitOfWork.studyRepository.GetAll();

            if (simplified)
            {

                var reducedStudiesToReturn = _mapper.Map<IEnumerable<Study>, IEnumerable<BasicStudyData>>(studies);
                return Ok(reducedStudiesToReturn);
            }

            var studiesToReturn = _mapper.Map<IEnumerable<Study>, IEnumerable<StudyData>>(studies);
            return Ok(studiesToReturn);
        }

        [HttpGet("{id}", Name = "GetStudy")]
        public async Task<IActionResult> GetStudy(int id)
        {
            var study = await _unitOfWork.studyRepository.Get(id);
            var data = _mapper.Map<StudyData>(study);
            return Ok(data);
        }

        [HttpGet("{id:int}/groups")]
        public async Task<IActionResult> GetStudyGroups(int id)
        {
            var study = await _unitOfWork.studyRepository.Get(id);
            var groups = study.StudyGroups;
            var groupsToReturn = _mapper.Map<IEnumerable<StudyGroup>, IEnumerable<BasicGroupData>>(groups);
            return Ok(groupsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Save(StudyData data)

        {
            var study = _mapper.Map<Study>(data);

            _unitOfWork.studyRepository.Add(study);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("GetStudy", new { id = study.Id }, study.Id);

        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if(_unitOfWork.studyRepository.Remove(Id))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }
    }
}