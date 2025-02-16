using Domain.Customer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Logo);

            builder.Property(c => c.CreateDate);

            builder.Property(c => c.UpdateDate);

            builder.HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Users)
                .WithOne(u => u.Customer)
                .HasForeignKey(u => u.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
