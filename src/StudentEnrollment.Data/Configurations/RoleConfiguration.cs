using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable("Roles", "identity");

        builder.HasData(new IdentityRole
            {
                Id = "7a40a1b6-00dc-4095-8bcd-88f6e2209559",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "6b0c7956-171c-4c71-808d-52aaf1e024a7",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}