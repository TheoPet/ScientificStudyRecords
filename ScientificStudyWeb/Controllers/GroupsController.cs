using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using System.Linq;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        [HttpGet("{groupId:int}/testSubjects")]
        public async Task<IActionResult> GetTestSubjects(int groupId)
        {
            var testSubjects = await _unitOfWork.testSubjectRepository.GetAll(g => g.GroupId.Equals(groupId));
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

        [HttpPut("{id:int}")]
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