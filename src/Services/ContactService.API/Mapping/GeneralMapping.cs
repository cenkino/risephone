using AutoMapper;
using ContactService.API.Domain.Entities;
using ContactService.API.Models;

namespace ContactService.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Contact, ContactVal>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoVal>().ReverseMap();
            CreateMap<Contact, ContactIndexVal>().ReverseMap();
            CreateMap<Contact, ContactEditVal>().ReverseMap();
        }
    }
}
