using AutoMapper;
using ContactService.API.Domain.Entities;
using ContactService.API.Infrastructure.Repository;
using ContactService.API.IntegrationEvents.Events;
using ContactService.API.Models;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RisePhoneApp.Shared.Core.Base;
using RisePhoneApp.Shared.Models.ResponseModels;
using System.Net;

namespace ContactService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : BaseController<IContactRepository>
    {
        public ContactsController(IContactRepository repository, IMapper mapper) : base(repository, mapper)
        {
          
        }

        [HttpGet("infos")]
        [ProducesResponseType(typeof(CustomResponse<IList<ContactVal>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContactInfos()
        {
            var list = await _repository.GetAllContactInfosAsync();

            IList<ContactInfoVal> infos = null;
            if (list != null && list.Any())
            {
                infos = _mapper.Map<IList<ContactInfoVal>>(list);
            }

            return CustomResponse<IList<ContactInfoVal>>.Success(infos, (int)HttpStatusCode.OK);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponse<IList<ContactIndexVal>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContacts()
        {
            var list = await _repository.GetAllContactsAsync();

            IList<ContactIndexVal> contacts = null;
            if (true)
            {
                contacts = _mapper.Map<IList<ContactIndexVal>>(list);
            }

            return CustomResponse<IList<ContactIndexVal>>.Success(contacts, (int)HttpStatusCode.OK);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomResponse<ContactVal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetContactByIdAsync(string id)
        {
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ContactVal>(contact);

            var contactInfos = await _repository.GetContactInfosByContactIdAsync(contact.Id);
            if (contactInfos != null && contactInfos.Any())
            {
                model.ContactInfos = _mapper.Map<IList<ContactInfoVal>>(contactInfos);
            }

            return CustomResponse<ContactVal>.Success(model, (int)HttpStatusCode.OK);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<ResultId<string>>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateContactAsync([FromBody] ContactEditVal model)
        {
            var newContact = _mapper.Map<Contact>(model);

            var _contact = await _repository.CreateContactAsync(newContact);

            if (_contact == null) return BadRequest();

            var result = new ResultId<string> { Id = _contact.Id };
            return CustomResponse<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
        }

        [HttpPost("info")]
        [ProducesResponseType(typeof(CustomResponse<ResultId<string>>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateContactInfoAsync([FromBody] ContactInfoVal model)
        {
            var newContactInfo = _mapper.Map<ContactInfo>(model);

            var _contactInfo = await _repository.CreateContactInfoAsync(newContactInfo);

            if (_contactInfo == null) return BadRequest();

            var result = new ResultId<string> { Id = _contactInfo.Id };
            return CustomResponse<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteContactByIdAsync(string id)
        {
            var result = await _repository.DeleteContactAsync(id);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("info/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteContactInfoByIdAsync(string id)
        {
            var result = await _repository.DeleteContactInfoAsync(id);

            return result ? Ok() : BadRequest();
        }


        [HttpPost("CreateContactReport")]
        [ProducesResponseType(typeof(CustomResponse<ReportCreatedIntegrationEvent>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateContactReport([FromBody] ReportStartedIntegrationEvent req)
        {


            var resultModel = new ReportCreatedIntegrationEvent(req.ReportId);

            var contactInfos = await _repository.GetAllContactInfosAsync();
            if (contactInfos == null || !contactInfos.Any())
            {
                
                return BadRequest();
            }

            var locations = contactInfos.Where(x => x.InfoType == ContactInfo.ContactInfoType.Location);
            if (locations == null || !locations.Any())
            {
                
                return BadRequest();
            }

            var distinctLocations = locations.Select(x => x.Value).Distinct();
            foreach (var location in distinctLocations)
            {
                var contacts = locations
                  .Where(x => x.Value == location)
                  .Select(x => x.ContactId)
                  .Distinct();

                var phoneNumbers = contactInfos
                  .Where(x => x.InfoType == ContactInfo.ContactInfoType.Phone && contacts.Contains(x.ContactId))
                  .Select(x => x.Value)
                  .Distinct()
                  .Count();

                resultModel.AddDetail(location, contacts.Count(), phoneNumbers);
            }

           return  CustomResponse<ReportCreatedIntegrationEvent>.Success(resultModel, (int)HttpStatusCode.Created);
        }

    }
}
