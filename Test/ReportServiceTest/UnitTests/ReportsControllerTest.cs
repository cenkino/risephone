using AutoMapper;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportService.API.Controllers;
using ReportService.API.Domain.Entities;
using ReportService.API.Infrastructure.Repository;
using ReportService.API.IntegrationEvents.Events;
using ReportService.API.Mapping;
using ReportService.API.Models;
using RisePhoneApp.Shared.Core.Base;
using RisePhoneApp.Shared.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportServiceTest.UnitTests
{
    public class ReportsControllerTest:BaseMock<IReportRepository,ReportsController,GeneralMapping>
    {

        private readonly Mock<IEventBus> _serviceBusMock;

        public ReportsControllerTest():base(new Mock<IEventBus>().Object) 
        {
            _serviceBusMock = new Mock<IEventBus>();
        }

        [Fact]
        public async Task Get_all_reports_OK()
        {
            //Arrange
            RepositoryM.Setup(x => x.GetAllReportsAsync())
              .Returns(Task.FromResult(GetReportsFake()));

            //Act
            var actionResult = await ControllerM.GetAllReports();

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

            RepositoryM.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult(fakeReport));

            _serviceBusMock.Setup(x => x.Publish(It.IsAny<ReportStartedIntegrationEvent>()));

            //Act
            var actionResult = await ControllerM.CreateReportAsync();

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
            RepositoryM.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult((Report?)null));

            //Act
            var actionResult = await ControllerM.CreateReportAsync();

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_report_by_id_OK()
        {
            //Arrange
            var fakeReportId = "887f1f77bcf86cd799439092";

            RepositoryM.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetReportById(fakeReportId)));

            //Act
            var actionResult = await ControllerM.GetReportByIdAsync(fakeReportId);

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

                RepositoryM.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult((Report?)null));

            //Act
            var actionResult = await ControllerM.GetReportByIdAsync(fakeReportId);

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
