using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System.Linq;
using AutoMapper;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TestSubjectController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TestSubjectController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetTestSubject")]
        public async Task<IActionResult> GetTestSubject(int id)
        {
            var testSubject = await _unitOfWork.testSubjectRepository.Get(id);
            var dataToReturn = _mapper.Map<TestSubjectData>(testSubject);
            return Ok(dataToReturn);
        }
        [HttpPost("Save")]
        public async Task<IActionResult> Save(TestSubjectData data)
        {         
            var testSubject = _mapper.Map<TestSubject>(data);
            testSubject.Study = null;
            testSubject.Group = null;
            testSubject.EntryTime = DateTime.Now;
            _unitOfWork.testSubjectRepository.Add(testSubject);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtRoute("GetTestSubject", new { id = testSubject.Id }, testSubject.Id);
        }


        /*[HttpPatch("Update")]
        public async Task<IActionResult> Update()
        {

        }*/

    }
}