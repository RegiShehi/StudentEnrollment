using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StudentEnrollment.Data.DbContext;

public class StudentEnrollmentDbContextFactory : IDesignTimeDbContextFactory<StudentEnrollmentDbContext>
{
    public StudentEnrollmentDbContext CreateDbContext(string[] args)
    {
        // var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<StudentEnrollmentDbContext>();
        var connectionString = config.GetConnectionString("StudentEnrollment");

        optionsBuilder.UseSqlServer(connectionString);

        return new StudentEnrollmentDbContext(optionsBuilder.Options);
    }
}