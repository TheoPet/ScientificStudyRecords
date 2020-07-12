using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;

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
    }
}