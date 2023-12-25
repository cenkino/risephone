using AutoMapper;
using ContactService.API.Controllers;
using ContactService.API.Domain.Entities;
using ContactService.API.Infrastructure.Repository;
using ContactService.API.Mapping;
using ContactService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RisePhoneApp.Shared.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactServiceTest.UnitTests
{
    public class ContactsControllerTest
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ContactsController _contactController;

        public ContactsControllerTest()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>()).CreateMapper();

            _contactController = new ContactsController(_contactRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_all_contact_infos_OK()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.GetAllContactInfosAsync())
              .Returns(Task.FromResult(GetContactInfosFake("907f1f77bcf86cd799439046")));

            //Act
            var actionResult = await _contactController.GetAllContactInfos();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<CustomResponse<IList<ContactInfoVal>>>(objectResult.Value);
        }

        [Fact]
        public async Task Get_all_contacts_OK()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.GetAllContactsAsync())
              .Returns(Task.FromResult(GetContactsFake()));

            //Act
            var actionResult = await _contactController.GetAllContacts();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<CustomResponse<IList<ContactIndexVal>>>(objectResult.Value);
        }

        [Fact]
        public async Task Get_contact_by_id_OK()
        {
            //Arrange
            var fakeContactId = "887f1f77bcf86cd799439092";

            _contactRepositoryMock.Setup(x => x.GetContactByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetContactById(fakeContactId)));

            //Act
            var actionResult = await _contactController.GetContactByIdAsync(fakeContactId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponse<ContactVal>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactId);
        }

        [Fact]
        public async Task Get_contact_by_id_not_found()
        {
            //Arrange
            var fakeContactId = "150f1f77bcf86cd799439092";

            _contactRepositoryMock.Setup(x => x.GetContactByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetContactById(fakeContactId)));

            //Act
            var actionResult = await _contactController.GetContactByIdAsync(fakeContactId);

            //Assert
            var objectResult = (NotFoundResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_contact_created()
        {
            //Arrange
            var fakeContactId = "887f1f77bcf86cd799439092";
            var fakeContact = GetContactById(fakeContactId);
            var model = _mapper.Map<ContactEditVal>(fakeContact);

            _contactRepositoryMock.Setup(x => x.CreateContactAsync(It.IsAny<Contact>()))
              .Returns(Task.FromResult(fakeContact));

            //Act
            var actionResult = await _contactController.CreateContactAsync(model);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponse<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactId);
        }

        [Fact]
        public async Task Create_contact_model_valid()
        {
            //Arrange
            var model = new ContactEditVal
            {
                Name = "",
                LastName = "lastname",
                Company = "company"
            };

            //Act
            var validationContext = new ValidationContext(model);

            var results = model.Validate(validationContext);

            //Assert
            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactEditVal.Name))));
        }

        [Fact]
        public async Task Create_contact_info_created()
        {
            //Arrange
            var fakeContactInfoId = "507f1f77bcf86cd799439011";
            var fakeContactInfo = GetContactInfoById(fakeContactInfoId);
            var model = _mapper.Map<ContactInfoVal>(fakeContactInfo);

            _contactRepositoryMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfo>()))
              .Returns(Task.FromResult(fakeContactInfo));

            //Act
            var actionResult = await _contactController.CreateContactInfoAsync(model);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponse<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactInfoId);
        }

        [Fact]
        public async Task Create_contact_info_model_valid()
        {
            //Arrange
            var model = new ContactInfoVal
            {
                ContactId = "",
                InfoType = (int)ContactInfo.ContactInfoType.Email,
                Value = ""
            };

            //Act
            var validationContext = new ValidationContext(model);

            var results = model.Validate(validationContext);

            //Assert

            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactInfoVal.Value))));
            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactInfoVal.ContactId))));
        }

        [Fact]
        public async Task Delete_contact_OK()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(true));

            //Act
            var actionResult = await _contactController.DeleteContactByIdAsync("887f1f77bcf86cd799439092");

            //Assert
            var objectResult = (OkResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_contact_bad_request()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(false));

            //Act
            var actionResult = await _contactController.DeleteContactByIdAsync("887f1f77bcf86cd799439092");

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_contact_info_OK()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactInfoAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(true));

            //Act
            var actionResult = await _contactController.DeleteContactInfoByIdAsync("236f1f77bcf86cd799439092");

            //Assert
            var objectResult = (OkResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_contact_info_bad_request()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactInfoAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(false));

            //Act
            var actionResult = await _contactController.DeleteContactInfoByIdAsync("236f1f77bcf86cd799439092");

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        private Contact GetContactById(string fakeContactId)
        {
            return GetContactsFake().FirstOrDefault(x => x.Id == fakeContactId);
        }

        private ContactInfo GetContactInfoById(string fakeContactInfoId)
        {
            return GetContactInfosFake("887f1f77bcf86cd799439092")
              .FirstOrDefault(x => x.Id == fakeContactInfoId);
        }

        private IList<Contact> GetContactsFake()
        {
            return new List<Contact>
      {
        new Contact("TestName", "TestLastName", "TestCompany")
        {
          Id = "887f1f77bcf86cd799439092"
        },
        new Contact("TestName2", "TestLastName2", "TestCompany2")
        {
          Id = "777f1f77bcf86cd799439011"
        },
      };
        }

        private IList<ContactInfo> GetContactInfosFake(string fakeContactId)
        {
            return new List<ContactInfo>
      {
        new ContactInfo(fakeContactId, ContactInfo.ContactInfoType.Email, "fake@email.com")
        {
          Id = "507f1f77bcf86cd799439011"
        },
        new ContactInfo(fakeContactId, ContactInfo.ContactInfoType.Location, "istanbul")
        {
          Id = "607f1f77bcf86cd799439022"
        },
      };
        }

    }
}
