using Data.Mapping;
using Domain.Address;
using Domain.Contracts;
using Domain.Customer;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    public class CustomerDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IUnitOfWork
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }

        public async Task<bool> Commit()
        {
            var entitiesAdded = new List<EntityEntry>();
            var entitiesModified = new List<EntityEntry>();
            var entitiesDeleted = new List<EntityEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entitiesAdded.Add(entry);
                        break;
                    case EntityState.Modified:
                        entitiesModified.Add(entry);
                        break;
                    case EntityState.Deleted:
                        entitiesDeleted.Add(entry);
                        break;
                }
            }

            var save = await SaveChangesAsync() > 0;

            return save;
        }

        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IEntityBase entityBase)
                {
                    if (entry.State == EntityState.Added)
                        entityBase.CreateDate = DateTime.Now;

                    if (entry.State == EntityState.Modified)
                        entityBase.UpdateDate = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
