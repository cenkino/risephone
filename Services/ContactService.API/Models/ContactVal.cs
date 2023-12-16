using ContactService.API.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactService.API.Models
{
    public class ContactVal: IValidatableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public IList<ContactInfoVal> ContactInfos { get; set; } = new List<ContactInfoVal>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Cannot be empty", new[] { "Name" });
            }
        }
    }
}
