using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);

            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.Email).HasMaxLength(256);

            builder.Property(u => u.CreateDate);

            builder.Property(u => u.UpdateDate);

            builder.HasOne(u => u.Customer)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CustomerId);
        }
    }
}
