using Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Address
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.")]
        [Display(Name = "Endereço")]
        public string Place { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.")]
        [Display(Name = "Cidade")]
        public string City { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo {0} deve ter 2 caracteres.")]
        [Display(Name = "Estado")]
        public string State { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O campo {0} deve ter 8 dígitos numéricos.")]
        [Display(Name = "CEP")]
        public string ZipCode { get; private set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CustomerId { get; private set; }
        public virtual Customer.Customer Customer { get; set; }

        public Address() { }

        public Address(string place, string neighborhood, string city, string state, string zipCode, int customerId)
        {
            Place = place;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            CustomerId = customerId;
            CreateDate = DateTime.UtcNow;
        }

        public void Update(string place, string neighborhood, string city, string state, string zipCode)
        {
            Place = place;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            UpdateDate = DateTime.UtcNow;
        }

        public void Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                throw new ValidationException($"Validation failed: {string.Join(", ", validationResults.Select(vr => vr.ErrorMessage))}");
            }
        }
    }
}
