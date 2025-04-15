using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace EFCore.EntityTypeConfigurations
{
    public class SubChoiceConfiguration : IEntityTypeConfiguration<SubChoice>
    {
        public void Configure(EntityTypeBuilder<SubChoice> builder)
        {
            builder.ToTable("SubChoices");

            builder.HasKey(u => u.SubChoiceId);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Name)
                .IsUnique();

            builder.HasOne(sc => sc.Choice)
            .WithMany(c => c.SubChoices)
            .HasForeignKey(sc => sc.ChoiceId);
        }
    }
}