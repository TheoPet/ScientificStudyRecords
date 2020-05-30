using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using System.Collections.Generic;
using ScientificStudiesRecord.DataObjects;
using System;
using AutoMapper;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public StudyController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(_context); 
        }

        [HttpGet("{id}", Name = "GetStudy")]
        public async Task<IActionResult> GetStudy(int Id)
        {
            var study = await _unitOfWork.studyRepository.Get(Id);
            var data = _mapper.Map<StudyData>(study);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudies()
        {
            var studies = await _unitOfWork.studyRepository.GetAll();
            var studiesToReturn = _mapper.Map<IEnumerable<Study>,IEnumerable<StudyData>>(studies);
            return Ok(studiesToReturn);
        }       
        [HttpPost("Save")]
        public async Task<IActionResult> Save(StudyData data)

        {
            var study = _mapper.Map<Study>(data);

            _unitOfWork.studyRepository.Add(study);
            _unitOfWork.taskRepository.AddRange(study.Tasks);
            foreach(var group in study.StudyGroups)
                _unitOfWork.groupRepository.Add(group.Group);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("GetStudy", new { id = study.Id }, study.Id);

        }
    
        [HttpDelete("{id}", Name = "Delete")]
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