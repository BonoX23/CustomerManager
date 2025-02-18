using Domain.Contracts;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Customer
{
    public class Customer : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Url(ErrorMessage = "URL do Logo inválida")]
        [Display(Name = "Logo (URL)")]
        public string Logo { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Address.Address> Addresses { get; set; }

        public Customer()
        {
            Users = new List<User>();
            Addresses = new List<Address.Address>();
        }

        public Customer(string name, string email, string logo) : this()
        {
            Name = name;
            Email = email;
            Logo = logo;
            CreateDate = DateTime.UtcNow;
        }

        public void Update(string name, string logo)
        {
            Name = name;
            Logo = logo;
            UpdateDate = DateTime.UtcNow;
        }

    }
}
