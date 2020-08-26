using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Data.Authorization;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
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

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredPaginatedResults([FromQuery] int pageSize,
                                                                     [FromQuery] int pageNumber,
                                                                     [FromQuery] string searchTerm)
        {
            var term = (!string.IsNullOrEmpty(searchTerm)) ? searchTerm : string.Empty;

            var parameters = new SearchParameters()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SearchTerm = term
            };

            var studies = await _unitOfWork.studyRepository.GetAllFiltered(parameters);
            var studiesToReturn = _mapper.Map<IEnumerable<Study>, IEnumerable<BasicData>>(studies);

            var metadata = new
            {
                pageSize = studies.PageSize,
                pageNumber = studies.CurrentPage,
                totalCount = studies.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(studiesToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudies([FromQuery] bool simplified)
        {
            var studies = await _unitOfWork.studyRepository.GetAll();

            if (simplified)
            {
                var simplifiedStudiesToReturn = _mapper.Map<IEnumerable<Study>, IEnumerable<BasicData>>(studies);
                return Ok(simplifiedStudiesToReturn);
            }

            var studiesToReturn = _mapper.Map<IEnumerable<Study>, IEnumerable<StudyData>>(studies);
            return Ok(studiesToReturn);
        }


        [HttpGet("{id:int}", Name = "GetStudy")]
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
            var groupsToReturn = _mapper.Map<IEnumerable<Group>, IEnumerable<BasicData>>(study.Groups);
            return Ok(groupsToReturn);
        }

        [HttpGet("{id:int}/tasks")]
        public async Task<IActionResult> GetStudyTasks(int id)
        {
            var study = await _unitOfWork.studyRepository.Get(id);
            var tasksToReturn = _mapper.Map<IEnumerable<Models.Task>, IEnumerable<BasicData>>(study.Tasks);
            return Ok(tasksToReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Save(StudyData data)

        {
            var study = _mapper.Map<Study>(data);

            _unitOfWork.studyRepository.Add(study);
            await _unitOfWork.SaveChangesAsync();

            var studyToReturn = _mapper.Map<StudyData>(study);

            return Ok(studyToReturn);
        }

        // [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudy(StudyData data, int id)
        {
            var studyToUpdate = await _unitOfWork.studyRepository.Get(id);

            if (!string.IsNullOrEmpty(data.Name))
            {
                studyToUpdate.Name = data.Name;
                await _unitOfWork.SaveChangesAsync();
                var studyToReturn = _mapper.Map<StudyData>(studyToUpdate);
                return Ok(studyToReturn);
            }

            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AddGroupOrTask(BasicData data, int id, bool addGroup)
        {
            var study = await _unitOfWork.studyRepository.Get(id);

            if (addGroup)
            {
                _unitOfWork.studyRepository.AddGroup(data.Name, study);
            }
            else
            {
                _unitOfWork.studyRepository.AddTask(data.Name, study);
            }

            await _unitOfWork.SaveChangesAsync();

            var studyToReturn = _mapper.Map<StudyData>(study);

            return Ok(studyToReturn);
        }

        [HttpDelete("{studyid:int}/{id:int}")]
        public async Task<IActionResult> DeleteGroupOrTask(int studyId, int id, bool deleteGroup)
        {

            var study = await _unitOfWork.studyRepository.Get(studyId);
            if (deleteGroup)
            {
                _unitOfWork.studyRepository.RemoveGroup(id, study);

            }
            else
            {
                _unitOfWork.studyRepository.RemoveTask(id, study);
            }

            await _unitOfWork.SaveChangesAsync();

            var studyToReturn = _mapper.Map<StudyData>(study);

            return Ok(studyToReturn);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (_unitOfWork.studyRepository.Remove(Id))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }
    }
}