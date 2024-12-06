using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection"));
});

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapGet("/courses", async (StudentEnrollmentDbContext context) => await context.Courses.ToListAsync());

app.MapGet("/courses/{id}", async (StudentEnrollmentDbContext context, int id) =>
{
    var course = await context.Courses.FindAsync(id);

    return course is null ? Results.NotFound() : Results.Ok(course);
});

app.MapPost("/courses", async (StudentEnrollmentDbContext context, Course course) =>
{
    await context.Courses.AddAsync(course);
    await context.SaveChangesAsync();

    return Results.Created($"/courses/{course.Id}", course);
});

app.MapPut("/courses/{id}", async (StudentEnrollmentDbContext context, Course courseDto, int id) =>
{
    var course = await context.Courses.FindAsync(id);

    if (course is null) return Results.NotFound();

    context.Courses.Update(courseDto);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/courses/{id}", async (StudentEnrollmentDbContext context, int id) =>
{
    var course = await context.Courses.FindAsync(id);

    if (course is null) return Results.NotFound();

    context.Courses.Remove(course);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();