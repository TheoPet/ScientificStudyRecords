using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TestSubjectsController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TestSubjectsController(ScientificStudiesRecordDbContext context, IMapper mapper)
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

        [HttpPost]
        public async Task<IActionResult> Save(TestSubjectData data)
        {         
            var testSubject = _mapper.Map<TestSubject>(data);
            testSubject.Study = null;
            testSubject.Group = null;
            _unitOfWork.testSubjectRepository.Add(testSubject);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtRoute("GetTestSubject", new { id = testSubject.Id }, testSubject.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool simplified, bool available)
        {
            var testSubjects = await _unitOfWork.testSubjectRepository.GetAll();
            var testSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<TestSubjectData>>(testSubjects);


            if (simplified)
            {
                if(available)
                {
                    var availableTestSubjects = testSubjects.Where(t=> t.StudyId == null);
                    testSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<TestSubjectData>>(availableTestSubjects);

                    return Ok(testSubjectsToReturn);
                }
                else
                {
                    var basicTestSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<BasicTestSubject>>(testSubjects);
                    return Ok(basicTestSubjectsToReturn);
                }

            }
            
            return Ok(testSubjectsToReturn);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody]TestSubjectData data)
        {
            var testSubject = _mapper.Map<TestSubject>(data);
            var testSubjectToUpdate = await _unitOfWork.testSubjectRepository.Get(testSubject.Id);
            
            //Teo: dodaj validaciju
            if(testSubjectToUpdate == null)
                return Ok("Element does not exist");
            testSubjectToUpdate.Name = testSubject.Name;
            testSubjectToUpdate.Surname = testSubject.Surname;
            testSubjectToUpdate.Comment = testSubject.Comment;
            testSubjectToUpdate.EntryTime = testSubject.EntryTime;

            testSubjectToUpdate.Study = testSubject.Study;
            testSubjectToUpdate.StudyId = testSubject.StudyId;

            testSubjectToUpdate.Group = testSubject.Group;
            testSubjectToUpdate.GroupId = testSubject.GroupId;

            await _unitOfWork.SaveChangesAsync();

            return Ok();

        }

    }
}