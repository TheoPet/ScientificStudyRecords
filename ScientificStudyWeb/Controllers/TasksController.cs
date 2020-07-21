using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TasksController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper; 

        public TasksController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context); 
            _mapper = mapper;
        }

        [HttpGet("{studyId:int}")]
        public async Task<IActionResult>GetTaskLookup(int studyId)
        {
            var tasks = await _unitOfWork.taskRepository.GetAll( t => t.StudyId.Equals(studyId));
            var tasksToReturn = _mapper.Map<IEnumerable<BasicData>>(tasks);
            return Ok(tasksToReturn);
        }
    }
}