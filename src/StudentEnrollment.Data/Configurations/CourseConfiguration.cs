using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configurations;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
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