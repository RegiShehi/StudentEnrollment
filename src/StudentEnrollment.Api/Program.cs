using Carter;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Configurations;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.DbContext;
using StudentEnrollment.Data.Repositories;
using StudentEnrollment.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection");

// Add services to the container.
builder.Services.AddDbContext<StudentEnrollmentDbContext>(options => { options.UseSqlServer(conn); });
builder.Services.AddCarter();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapCarter();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapCourseEndpoints();
app.MapEnrollmentEndpoints();

app.Run();