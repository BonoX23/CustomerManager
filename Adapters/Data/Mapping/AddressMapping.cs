using Domain.Address;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Place)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Neighborhood)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(8);

            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId);
        }
    }
}
