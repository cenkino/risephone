namespace ContactService.API.Domain.Entities
{
    public class Contact
    {
        public Contact(string name, string lastName, string company)
        {
            Name = name;
            LastName = lastName;
            Company = company;
        }

        
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
