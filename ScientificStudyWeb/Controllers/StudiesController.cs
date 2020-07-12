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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(StudyData data)
        {
            var study =  _mapper.Map<Study>(data);
            var studyToUpdate = await _unitOfWork.studyRepository.Get(study.Id);

            studyToUpdate.Name = study.Name;
            _unitOfWork.studyRepository.UpdateTasks(studyToUpdate.Tasks,study.Tasks);
            _unitOfWork.studyRepository.UpdateStudyGroups(studyToUpdate.StudyGroups, study.StudyGroups);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{studyId:int}/{Id:int}")]
        public async Task<IActionResult> DeleteGroup(int studyId, int Id, bool deleteGroup){

            var studyToUpdate = await _unitOfWork.studyRepository.Get(studyId);
            if (deleteGroup)
            {
                var groupToDelete = studyToUpdate.StudyGroups.FirstOrDefault(g => g.GroupId.Equals(Id));
                studyToUpdate.StudyGroups.Remove(groupToDelete);
            }else
            {
                var taskToDelete = studyToUpdate.Tasks.FirstOrDefault(t => t.Id.Equals(Id));
                studyToUpdate.Tasks.Remove(taskToDelete);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
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