using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.Data.Interfaces;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Models;
using System;
using System.IO;
using DinkToPdf;
using ScientificStudyWeb.Helpers.PDFHelper;
using ScientificStudyWeb.Helpers;
using DinkToPdf.Contracts;
using static ScientificStudiesRecord.Startup;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Data.Authorization;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ExperimentsController : ControllerBase
    {
        private readonly ScientificStudiesRecordDbContext _context;

        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        private IConverter _converter;

        public ExperimentsController(ScientificStudiesRecordDbContext context, IMapper mapper)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            CustomAssemblyLoadContext assemblyContext = new CustomAssemblyLoadContext();
            assemblyContext.LoadUnmanagedLibrary("/home/teo/Projects/Master_rad/ScientificStudyRecords/ScientificStudyWeb/bin/Debug/netcoreapp3.1/libwkhtmltox.so");
            _converter = new SynchronizedConverter(new PdfTools());
            ;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExperimentData experiment)
        {
            var experimentToSave = _mapper.Map<Experiment>(experiment);
            experimentToSave.Task = null;
            experimentToSave.TestSubject = null;


            _unitOfWork.experimentRepository.Add(experimentToSave);
            await _unitOfWork.SaveChangesAsync();

            var experimentToReturn = _mapper.Map<ExperimentData>(experimentToSave);
            experimentToReturn.Task = experiment.Task;
            experimentToReturn.TestSubject = experiment.TestSubject;

            return Ok(experimentToReturn);
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPut]
        public async Task<IActionResult> Update(ExperimentData experiment)
        {
            var experimentToUpdate = await _unitOfWork.experimentRepository.Get(experiment.Id.Value);
            experimentToUpdate.Time = experiment.Time;
            experimentToUpdate.Comment = experiment.Comment;

            await _unitOfWork.SaveChangesAsync();

            var experimentToReturn = _mapper.Map<ExperimentData>(experimentToUpdate);
            return Ok(experimentToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExperiment(int id)
        {
            var experiment = await _unitOfWork.experimentRepository.Get(id);
            var experimentToReturn = _mapper.Map<ExperimentData>(experiment);

            return Ok(experimentToReturn);
        }

        [HttpGet("filtered/studies/{studyId:int}")]
        public async Task<IActionResult> GetExperimentsFilteredByStudy(int studyId, [FromQuery] int pageSize,
                                                                     [FromQuery] int pageNumber)
        {
            var parameters = new PaginationParameters()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };

            var experiments = await _unitOfWork.experimentRepository.GetAllFilteredByStudy(parameters, studyId);
            var experimentsToReturn = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);

            var metadata = new
            {
                pageSize = experiments.PageSize,
                pageNumber = experiments.CurrentPage,
                totalCount = experiments.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(experimentsToReturn);
        }

        [HttpGet("filtered/groups/{groupId:int}")]
        public async Task<IActionResult> GetExperimentsFilteredByGroup(int groupId, [FromQuery] int pageSize,
                                                                                   [FromQuery] int pageNumber)
        {
            var parameters = new PaginationParameters()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };

            var experiments = await _unitOfWork.experimentRepository.GetAllFilteredByGroup(parameters, groupId);
            var experimentsToReturn = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);

            var metadata = new
            {
                pageSize = experiments.PageSize,
                pageNumber = experiments.CurrentPage,
                totalCount = experiments.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(experimentsToReturn);
        }

        [HttpGet("filtered/testsubjects/{testSubjectId:int}")]
        public async Task<IActionResult> GetExperimentsFilteredByTestSubject(int testSubjectId, [FromQuery] int pageSize,
                                                                                               [FromQuery] int pageNumber)
        {
            var parameters = new PaginationParameters()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };

            var experiments = await _unitOfWork.experimentRepository.GetAllFilteredByTestSubject(parameters, testSubjectId);
            var experimentsToReturn = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);

            var metadata = new
            {
                pageSize = experiments.PageSize,
                pageNumber = experiments.CurrentPage,
                totalCount = experiments.TotalCount
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(experimentsToReturn);
        }

        [HttpGet("export/study/{studyId:int}")]
        public async Task<IActionResult> ExportExperimentsReportByStudy(int studyId, [FromQuery] string studyName)
        {
            var experiments = await _unitOfWork.experimentRepository.GetAll(e => e.StudyId == studyId);
            var experimentsToExport = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);
            var pdfHelp = new PDFHelper();
            var globalSettings = pdfHelp.SetGlobalSettings("Experiment report - filtered by study");
            var objectSettings = pdfHelp.SetObjectSettings();
            var templateGenerator = new TemplateGenerator();
            objectSettings.HtmlContent = templateGenerator.GetHTMLStringForExperimentReport(Utility.ReportType.FilteredByStudy,
                                                                                            studyName, experimentsToExport);

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }

        [HttpGet("export/group/{groupId:int}")]
        public async Task<IActionResult> ExportExperimentsReportByGroup(int groupId, [FromQuery] string groupName)
        {
            var experiments = await _unitOfWork.experimentRepository.GetAll(e => e.GroupId == groupId);
            var experimentsToExport = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);
            var pdfHelp = new PDFHelper();
            var templateGenerator = new TemplateGenerator();

            var globalSettings = pdfHelp.SetGlobalSettings("Experiment report - filtered by group");
            var objectSettings = pdfHelp.SetObjectSettings();
            objectSettings.HtmlContent = templateGenerator.GetHTMLStringForExperimentReport(Utility.ReportType.FilteredByGroup,
                                                                                            groupName, experimentsToExport);

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }

        [HttpGet("export/testsubject/{testSubjectId:int}")]
        public async Task<IActionResult> ExportExperimentsReportByTestSubject(int testSubjectId, [FromQuery] string testSubjectName)
        {
            var experiments = await _unitOfWork.experimentRepository.GetAll(e => e.TestSubjectId == testSubjectId);
            var experimentsToExport = _mapper.Map<IEnumerable<Experiment>, IEnumerable<ReportExperimentData>>(experiments);
            var pdfHelp = new PDFHelper();
            var templateGenerator = new TemplateGenerator();
            var globalSettings = pdfHelp.SetGlobalSettings("Experiment report - filtered by test subject");
            var objectSettings = pdfHelp.SetObjectSettings();
            objectSettings.HtmlContent = templateGenerator.GetHTMLStringForExperimentReport(Utility.ReportType.FilteredByTestSubject,
                                                                                            testSubjectName, experimentsToExport);

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (_unitOfWork.experimentRepository.Remove(Id))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }
    }
}