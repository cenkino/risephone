namespace ContactService.API.Domain.Entities
{
    public class ContactInfo
    {
        public ContactInfo() { }

        public ContactInfo(string contactId, ContactInfoType infoType, string value)
        {
            ContactId = contactId;
            InfoType = infoType;
            Value = value;
        }

        
        public string Id { get; set; }
        
        public string ContactId { get; set; }
        public ContactInfoType InfoType { get; set; }
        public string Value { get; set; }

        public enum ContactInfoType
        {
            Phone = 0,
            Email = 1,
            Location = 2
        }
    }
}
