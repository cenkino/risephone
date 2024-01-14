using System.ComponentModel.DataAnnotations;
using static ContactService.API.Domain.Entities.ContactInfo;

namespace ContactService.API.Models
{
    public class ContactInfoVal:IValidatableObject
    {
        public string? Id { get; set; } = null;
        public string ContactId { get; set; }
        public int InfoType { get; set; }
        public string Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Value))
            {
                yield return new ValidationResult("Cannot be empty", new[] { nameof(Value) });
            }

            if (string.IsNullOrEmpty(ContactId))
            {
                yield return new ValidationResult("Cannot be empty", new[] { nameof(ContactId) });
            }
        }
    }
}
