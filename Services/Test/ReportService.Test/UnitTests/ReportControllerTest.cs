using AutoMapper;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportService.API;
using ReportService.API.Controllers;
using ReportService.API.Domain.Entities;
using ReportService.API.Infrastructure.Repository;
using ReportService.API.IntegrationEvents.Events;
using ReportService.API.Mapping;
using ReportService.API.Models;
using RisePhoneApp.Shared.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportService.Test.UnitTests
{
    public class ReportsControllerTest
    {
        private readonly Mock<IReportRepository> _reportRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IEventBus> _serviceBusMock;
        private readonly ReportsController _reportController;

        public ReportsControllerTest()
        {
            _reportRepositoryMock = new Mock<IReportRepository>();
            _serviceBusMock = new Mock<IEventBus>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>()).CreateMapper();

            _reportController = new ReportsController(_reportRepositoryMock.Object, _mapper, _serviceBusMock.Object);
        }

        [Fact]
        public async Task Get_all_reports_OK()
        {
            //Arrange
            _reportRepositoryMock.Setup(x => x.GetAllReportsAsync())
              .Returns(Task.FromResult(GetReportsFake()));

            //Act
            var actionResult = await _reportController.GetAllReports();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<CustomResponse<IList<ReportIndexVal>>>(objectResult.Value);
        }

        [Fact]
        public async Task Create_report_created()
        {
            //Arrange
            var fakeReportId = "887f1f77bcf86cd799439092";
            var fakeReport = GetReportById(fakeReportId);

            _reportRepositoryMock.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult(fakeReport));

            _serviceBusMock.Setup(x => x.Publish(It.IsAny<ReportStartedIntegrationEvent>()));

            //Act
            var actionResult = await _reportController.CreateReportAsync();

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponse<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeReportId);
        }

        [Fact]
        public async Task Create_report_repository_return_null()
        {
            //Arrange
            _reportRepositoryMock.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult((Report?)null));

            //Act
            var actionResult = await _reportController.CreateReportAsync();

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_report_by_id_OK()
        {
            //Arrange
            var fakeReportId = "887f1f77bcf86cd799439092";

            _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetReportById(fakeReportId)));

            //Act
            var actionResult = await _reportController.GetReportByIdAsync(fakeReportId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponse<ReportVal>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeReportId);
        }

        [Fact]
        public async Task Get_report_by_id_not_found()
        {
            //Arrange
            var fakeReportId = "150f1f77bcf86cd799439092";

            _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult((Report?)null));

            //Act
            var actionResult = await _reportController.GetReportByIdAsync(fakeReportId);

            //Assert
            var objectResult = (NotFoundResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        private Report GetReportById(string fakeReportId)
        {
            return GetReportsFake().FirstOrDefault(x => x.Id == fakeReportId);
        }

        private IList<Report> GetReportsFake()
        {
            return new List<Report>
      {
        new Report(DateTime.UtcNow, Report.ReportStatus.Completed)
        {
          Id = "887f1f77bcf86cd799439092"
        },
        new Report(DateTime.UtcNow, Report.ReportStatus.Preparing)
        {
          Id = "777f1f77bcf86cd799439011"
        },
      };
        }

    }
}
