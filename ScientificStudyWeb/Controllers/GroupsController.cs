using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using System.Linq;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Data.Authorization;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork;

        public GroupsController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(_context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool lookup)
        {
            var groups = await _unitOfWork.groupRepository.GetAll();

            if (lookup)
            {
                var groupsLookupToReturn = _mapper.Map<IEnumerable<BasicData>>(groups);
                return Ok(groupsLookupToReturn);
            }

            var groupsToReturn = _mapper.Map<IEnumerable<GroupData>>(groups);

            return Ok(groupsToReturn);
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

            var groups = await _unitOfWork.groupRepository.GetAllFiltered(parameters);
            var groupsToReturn = _mapper.Map<IEnumerable<Group>, IEnumerable<BasicData>>(groups);

            var metadata = new
            {
                pageSize = groups.PageSize,
                pageNumber = groups.CurrentPage,
                totalCount = groups.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(groupsToReturn);
        }

        [HttpGet("{id:int}/testSubjects")]
        public async Task<IActionResult> GetTestSubjects(int id)
        {
            var testSubjects = await _unitOfWork.testSubjectRepository.GetAll(g => g.GroupId.Equals(id));
            var testSubjectsToReturn = _mapper.Map<IEnumerable<BasicTestSubject>>(testSubjects);
            return Ok(testSubjectsToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _unitOfWork.groupRepository.Get(id);
            var groupToReturn = _mapper.Map<GroupData>(group);

            return Ok(groupToReturn);
        }

        [HttpDelete("{groupId:int}/{id:int}")]
        public async Task<IActionResult> RemoveTestSubject(int groupId, int id)
        {
            var testSubjectToRemove = await _unitOfWork.testSubjectRepository.Get(id);
            testSubjectToRemove.StudyId = null;
            testSubjectToRemove.GroupId = null;
            await _unitOfWork.SaveChangesAsync();

            var group = await _unitOfWork.groupRepository.Get(groupId);
            var groupToReturn = _mapper.Map<GroupData>(group);

            return Ok(groupToReturn);
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_unitOfWork.groupRepository.Remove(id))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup(GroupData data)
        {
            var groupToUpdate = await _unitOfWork.groupRepository.Get(data.Id);
            groupToUpdate.Name = data.Name;
            await _unitOfWork.SaveChangesAsync();

            var groupToReturn = _mapper.Map<GroupData>(groupToUpdate);
            return Ok(groupToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupData group)
        {
            var groupToAdd = _mapper.Map<Group>(group);
            groupToAdd.Study = null;
            _unitOfWork.groupRepository.Add(groupToAdd);
            await _unitOfWork.SaveChangesAsync();

            var groupToReturn = _mapper.Map<GroupData>(groupToAdd);
            groupToReturn.Study = group.Study;
            return Ok(groupToReturn);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AddTestSubject(int id, TestSubjectData data)
        {
            var group = await _unitOfWork.groupRepository.Get(id);

            var testSubjectToAdd = _mapper.Map<TestSubject>(data);
            testSubjectToAdd.Study = null;
            testSubjectToAdd.Group = null;
            testSubjectToAdd.StudyId = group.StudyId;
            testSubjectToAdd.GroupId = id;

            group.TestSubjects.Add(testSubjectToAdd);

            await _unitOfWork.SaveChangesAsync();
            var groupToReturn = _mapper.Map<GroupData>(group);
            return Ok(groupToReturn);

        }
    }
}