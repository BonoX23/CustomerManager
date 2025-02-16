using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace Domain.Entities
{
    public class User : IdentityUser<int>, IEntityBase
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Ativo { get; private set; }
        public int CustomerId { get; set; }
        public virtual Customer.Customer Customer { get; set; }

        public User()
        {
        }

        /// <summary>
        /// Criar Usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public User(string login, string password)
        {
            UserName = login;
            var passwordHasher = new PasswordHasher<User>();
            PasswordHash = passwordHasher.HashPassword(this, password);
            CreateDate = DateTime.UtcNow;
            Ativo = true;
        }

        /// <summary>
        /// Atualizar senha
        /// </summary>
        /// <param name="password"></param>
        public void UpdatePassword(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        /// <summary>
        /// Atualizar status
        /// </summary>
        /// <param name="ativo"></param>
        public void UpdateStatus(bool ativo)
        {
            Ativo = ativo;
        }

        /// <summary>
        /// Associar Cliente
        /// </summary>
        /// <param name="customerId"></param>
        public void AssociarCliente(int customerId)
        {
            CustomerId = customerId;
        }

        /// <summary>
        /// Confirmar Email
        /// </summary>
        public void ConfirmarEmail()
        {
            EmailConfirmed = true;
        }
    }
}
