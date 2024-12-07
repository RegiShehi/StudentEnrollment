using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Data.DbContext;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Student",
                async (StudentEnrollmentDbContext context) => await context.Students.ToListAsync())
            .WithTags(nameof(Student))
            .WithName("GetAllStudents")
            .Produces<List<Student>>();

        routes.MapGet("/api/Student/{id}", async (int id, StudentEnrollmentDbContext context) =>
            {
                var student = await context.Students.FindAsync(id);

                return student == null ? Results.NotFound() : Results.Ok(student);
            })
            .WithTags(nameof(Student))
            .WithName("GetStudentById")
            .Produces<Student>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Student/{id}",
                async (int id, Student student, StudentEnrollmentDbContext context) =>
                {
                    var foundModel = await context.Students.FindAsync(id);

                    if (foundModel is null) return Results.NotFound();

                    context.Students.Update(student);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                })
            .WithTags(nameof(Student))
            .WithName("UpdateStudent")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Student/", async (Student student, StudentEnrollmentDbContext context) =>
            {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();

                return Results.Created($"/Students/{student.Id}", student);
            })
            .WithTags(nameof(Student))
            .WithName("CreateStudent")
            .Produces<Student>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Student/{id}",
                async (int id, StudentEnrollmentDbContext context) =>
                {
                    var student = await context.Students.FindAsync(id);

                    if (student == null) return Results.NotFound();

                    context.Students.Remove(student);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                })
            .WithTags(nameof(Student))
            .WithName("DeleteStudent")
            .Produces<Student>()
            .Produces(StatusCodes.Status404NotFound);
    }
}