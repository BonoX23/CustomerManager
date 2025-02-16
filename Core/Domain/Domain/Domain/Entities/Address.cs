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
        public string Place { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo {0} deve ter 2 caracteres.")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O campo {0} deve ter 8 dígitos numéricos.")]
        [Display(Name = "CEP")]
        public string ZipCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer.Customer Customer { get; set; }
    }
}
