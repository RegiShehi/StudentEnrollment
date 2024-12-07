using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment.Data.Configurations;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(50); // Set max length to 50

        builder.HasData(new Course
            {
                Id = 1,
                Title = "C# Course",
                Credits = 3
            },
            new Course
            {
                Id = 2,
                Title = ".NET Course",
                Credits = 5
            }
        );
    }
}