using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.EntityTypeConfigurations
{
    public class UserChoiceConfiguration : IEntityTypeConfiguration<UserChoice>
    {
        public void Configure(EntityTypeBuilder<UserChoice> builder)
        {
            builder.ToTable("UserChoices");

            builder.HasKey(u => u.UserChoiceId);

            builder.HasOne(uc => uc.User)
            .WithMany(u => u.Choices)
            .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.SubChoice)
            .WithMany()
            .HasForeignKey(uc => uc.SubChoiceId);
        }
    }
}
