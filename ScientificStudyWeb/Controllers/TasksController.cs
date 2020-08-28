using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Authorization;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _unitOfWork.taskRepository.Get(id);
            var taskToReturn = _mapper.Map<TaskData>(task);

            return Ok(taskToReturn);
        }

        // [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (_unitOfWork.taskRepository.Remove(Id))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }
    }
}