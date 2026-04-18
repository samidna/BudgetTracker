using BudgetTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetTracker.Infrastructure.Data.Configurations;
public class AuthConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.TotalBalance)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

        builder.Property(u => u.Currency)
               .HasMaxLength(3)
               .IsRequired()
               .HasDefaultValue("AZN");
    }
}
