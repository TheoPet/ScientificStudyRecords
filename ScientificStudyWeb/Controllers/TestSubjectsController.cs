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
using ScientificStudyWeb.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Data.Authorization;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("{id}", Name = "GetTestSubject")]
        public async Task<IActionResult> GetTestSubject(int id)
        {
            var testSubject = await _unitOfWork.testSubjectRepository.Get(id);

            var dataToReturn = _mapper.Map<TestSubjectData>(testSubject);
            return Ok(dataToReturn);
        }

        [HttpGet("{id:int}/groups/{groupId:int}", Name = "GetTestSubjectWithFilteredExperiments")]
        public async Task<IActionResult> GetTestSubjectWithFilteredExperiments(int id, int groupId)
        {
            var testSubject = await _unitOfWork.testSubjectRepository.GetWithFilteredExperiments(id, groupId);

            var dataToReturn = _mapper.Map<TestSubjectData>(testSubject);
            return Ok(dataToReturn);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredTestSubjects([FromQuery] int pageSize,
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

            var testSubjects = await _unitOfWork.testSubjectRepository.GetAllFiltered(parameters);
            var testSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<BasicTestSubject>>(testSubjects);

            var metadata = new
            {
                pageSize = testSubjects.PageSize,
                pageNumber = testSubjects.CurrentPage,
                totalCount = testSubjects.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(testSubjectsToReturn);
        }

        [HttpGet("studies/{studyId:int}")]
        public async Task<IActionResult> GetTestSubjectsFromSameStudyGroup(int studyId)
        {
            var testSubjects = await _unitOfWork.testSubjectRepository.GetAll(t => t.StudyId == studyId);
            var testSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<BasicTestSubject>>(testSubjects);

            return Ok(testSubjectsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Save(TestSubjectData testSubject)
        {
            var testSubjectToAdd = _mapper.Map<TestSubject>(testSubject);
            testSubjectToAdd.Study = null;
            testSubjectToAdd.Group = null;
            _unitOfWork.testSubjectRepository.Add(testSubjectToAdd);
            await _unitOfWork.SaveChangesAsync();

            var testSubjectToReturn = _mapper.Map<TestSubjectData>(testSubjectToAdd);
            testSubjectToReturn.Study = testSubject.Study;
            testSubjectToReturn.Group = testSubject.Group;
            return Ok(testSubjectToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool simplified, bool available)
        {
            var testSubjects = await _unitOfWork.testSubjectRepository.GetAll();
            var testSubjectsToReturn = _mapper.Map<IEnumerable<TestSubject>, IEnumerable<TestSubjectData>>(testSubjects);


            if (simplified)
            {
                if (available)
                {
                    var availableTestSubjects = testSubjects.Where(t => t.StudyId == null);
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

        [Authorize(Policy = Policies.Admin)]
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

        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(TestSubjectData testSubject)
        {
            var testSubjectToUpdate = await _unitOfWork.testSubjectRepository.Get(testSubject.Id.Value);

            //Teo: dodaj validaciju
            if (testSubjectToUpdate == null)
                return Ok("Element does not exist");

            testSubjectToUpdate.Name = testSubject.Name;
            testSubjectToUpdate.Surname = testSubject.Surname;
            testSubjectToUpdate.Comment = testSubject.Comment;
            testSubjectToUpdate.EntryTime = Convert.ToDateTime(testSubject.EntryTime);

            await _unitOfWork.SaveChangesAsync();

            var testSubjectToReturn = _mapper.Map<TestSubjectData>(testSubjectToUpdate);
            return Ok(testSubjectToReturn);

        }

        [HttpPut("{id:int}/assignStudyAndGroup")]
        public async Task<IActionResult> AssignStudyAndGroup(TestSubjectData testSubject)
        {
            var testSubjectToUpdate = await _unitOfWork.testSubjectRepository.Get(testSubject.Id.Value);

            //Teo: dodaj validaciju
            if (testSubjectToUpdate == null)
                return Ok("Element does not exist");

            testSubjectToUpdate.StudyId = testSubject.Study.Id;
            testSubjectToUpdate.GroupId = testSubject.Group.Id;
            await _unitOfWork.SaveChangesAsync();

            var testSubjectToReturn = _mapper.Map<TestSubjectData>(testSubjectToUpdate);
            return Ok(testSubjectToReturn);

        }

    }
}