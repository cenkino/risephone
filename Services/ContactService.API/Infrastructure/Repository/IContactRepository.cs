using ContactService.API.Domain.Entities;
using RisePhoneApp.Shared.Core.Base;

namespace ContactService.API.Infrastructure.Repository
{
    public interface IContactRepository:IBaseRepository
    {
        Task<bool> GetContactsHasDocumentForDummyDataAsync();
        Task<IList<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(string id);
        Task<Contact> CreateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(string id);
        Task<ContactInfo> CreateContactInfoAsync(ContactInfo contactInfo);
        Task<bool> DeleteContactInfoAsync(string id);
        Task<IList<ContactInfo>> GetAllContactInfosAsync();
        Task<IList<ContactInfo>> GetContactInfosByContactIdAsync(string contactId);
    }
}
