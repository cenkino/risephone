﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.API.Domain.Entities;
using ReportService.API.Infrastructure.Repository;
using ReportService.API.Models;
using RisePhoneApp.Shared.Models.ResponseModels;
using System.Net;

namespace ReportService.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        

        public ReportsController(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponse<IList<ReportIndexVal>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllReports()
        {
            var list = await _repository.GetAllReportsAsync();

            IList<ReportIndexVal> reports = null;
            if (list != null && list.Any())
            {
                reports = _mapper.Map<IList<ReportIndexVal>>(list);
            }

            return CustomResponse<IList<ReportIndexVal>>.Success(reports, (int)HttpStatusCode.OK);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<ResultId<string>>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateReportAsync()
        {
            var newReport = await _repository.CreateReportAsync();

            if (newReport == null) return BadRequest();

            

            var result = new ResultId<string> { Id = newReport.Id };
            return CustomResponse<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomResponse<ReportVal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReportByIdAsync(string id)
        {
            var report = await _repository.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ReportVal>(report);

            var details = await _repository.GetDetailsByReportIdAsync(report.Id);
            if (details != null && details.Any())
            {
                model.Details = _mapper.Map<IList<ReportDetailsVal>>(details);
            }

            return CustomResponse<ReportVal>.Success(model, (int)HttpStatusCode.OK);
        }

    }
}
