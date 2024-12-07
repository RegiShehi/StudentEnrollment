using AutoMapper;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Models;
using StudentEnrollment.DTOs.Student;

namespace StudentEnrollment.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Student", async (IStudentRepository repo, IMapper mapper) =>
            {
                var students = await repo.GetAllAsync();
                var data = mapper.Map<List<StudentDto>>(students);
                return data;
            })
            .WithTags(nameof(Student))
            .WithName("GetAllStudents")
            .Produces<List<StudentDto>>();

        routes.MapGet("/api/Student/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
            {
                var model = await repo.GetAsync(id);

                return model is null ? Results.NotFound() : Results.Ok(mapper.Map<StudentDto>(model));
            })
            .WithTags(nameof(Student))
            .WithName("GetStudentById")
            .Produces<StudentDto>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapGet("/api/Student/GetDetails/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
            {
                var model = await repo.GetStudentDetails(id);

                return model is null ? Results.NotFound() : Results.Ok(mapper.Map<StudentDetailsDto>(model));
            })
            .WithTags(nameof(Student))
            .WithName("GetStudentDetailsById")
            .Produces<StudentDetailsDto>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Student/{id}",
                async (int id, StudentDto studentDto, IStudentRepository repo, IMapper mapper) =>
                {
                    var foundModel = await repo.GetAsync(id);

                    if (foundModel is null) return Results.NotFound();
                    //update model properties here
                    mapper.Map(studentDto, foundModel);
                    await repo.UpdateAsync(foundModel);
                    return Results.NoContent();
                })
            .WithTags(nameof(Student))
            .WithName("UpdateStudent")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Student/", async (CreateStudentDto studentDto, IStudentRepository repo, IMapper mapper) =>
            {
                var student = mapper.Map<Student>(studentDto);
                await repo.AddAsync(student);
                return Results.Created($"/Students/{student.Id}", student);
            })
            .WithTags(nameof(Student))
            .WithName("CreateStudent")
            .Produces<Student>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Student/{id}",
                async (int id, IStudentRepository repo) =>
                    await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
            .WithTags(nameof(Student))
            .WithName("DeleteStudent")
            .Produces<Student>()
            .Produces(StatusCodes.Status404NotFound);
    }
}