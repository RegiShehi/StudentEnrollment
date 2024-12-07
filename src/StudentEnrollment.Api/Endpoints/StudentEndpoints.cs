using AutoMapper;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Models;
using StudentEnrollment.DTOs.Student;

namespace StudentEnrollment.Endpoints;

public class StudentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/students").WithTags(nameof(Student));

        group.MapGet("", GetAllStudents)
            .WithName(nameof(GetAllStudents))
            .Produces<List<StudentDto>>();

        group.MapGet("{id}", GetStudentById)
            .WithName(nameof(GetStudentById))
            .Produces<StudentDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("{id}", UpdateStudent)
            .WithName(nameof(UpdateStudent))
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("", CreateStudent)
            .WithName(nameof(CreateStudent))
            .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("{id}", DeleteStudent)
            .WithName(nameof(DeleteStudent))
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
    }

    private static async Task<IResult> GetAllStudents(IStudentRepository repo, IMapper mapper)
    {
        var students = await repo.GetAllAsync();
        var data = mapper.Map<List<StudentDto>>(students);

        return TypedResults.Ok(data);
    }

    private static async Task<Results<NotFound, Ok<StudentDto>>> GetStudentById(int id, IStudentRepository repo,
        IMapper mapper)
    {
        var model = await repo.GetAsync(id);

        return model is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<StudentDto>(model));
    }

    private static async Task<Results<NoContent, NotFound>> UpdateStudent(int id, StudentDto studentDto,
        IStudentRepository repo,
        IMapper mapper)
    {
        var foundModel = await repo.GetAsync(id);

        if (foundModel is null) return TypedResults.NotFound();

        mapper.Map(studentDto, foundModel);

        await repo.UpdateAsync(foundModel);

        return TypedResults.NoContent();
    }

    private static async Task<IResult> CreateStudent(CreateStudentDto studentDto, IStudentRepository repo,
        IMapper mapper)
    {
        var student = mapper.Map<Student>(studentDto);

        await repo.AddAsync(student);

        return TypedResults.Created($"/Students/{student.Id}", student);
    }

    private static async Task<Results<NoContent, NotFound>> DeleteStudent(int id, IStudentRepository repo)
    {
        return await repo.DeleteAsync(id) ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}