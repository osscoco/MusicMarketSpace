using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Identity;
using Models;

namespace EFCore.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Pseudo)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Pseudo)
                .IsUnique();

            builder.Property(u => u.Mail)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Mail)
                .IsUnique();

            builder.Property(u => u.PasswordHashed)
                .IsRequired();

            builder.Property(u => u.ContactPhone)
                .HasMaxLength(10);

            builder.HasIndex(u => u.ContactPhone)
                .IsUnique();

            builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        }
    }
}
