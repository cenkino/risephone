using AutoMapper;
using ContactService.API.Domain.Entities;
using ContactService.API.Infrastructure.Repository;
using ContactService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RisePhoneApp.Shared.Models.ResponseModels;
using System.Net;

namespace ContactService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
