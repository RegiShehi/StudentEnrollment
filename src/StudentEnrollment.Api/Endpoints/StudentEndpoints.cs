using AutoMapper;
using Carter;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Models;
using StudentEnrollment.DTOs.Student;

namespace StudentEnrollment.Endpoints;

public class StudentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/students");

        group.MapGet("", GetAllStudents)
            .WithTags(nameof(Student))
            .WithName(nameof(GetAllStudents))
            .Produces<List<StudentDto>>();

        group.MapGet("{id}", GetStudentById)
            .WithTags(nameof(Student))
            .WithName(nameof(GetStudentById))
            .Produces<StudentDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("{id}", UpdateStudent)
            .WithTags(nameof(Student))
            .WithName(nameof(UpdateStudent))
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("", CreateStudent)
            .WithTags(nameof(Student))
            .WithName(nameof(CreateStudent))
            .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("{id}", DeleteStudent)
            .WithTags(nameof(Student))
            .WithName(nameof(DeleteStudent))
            .Produces<Student>()
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAllStudents(IStudentRepository repo, IMapper mapper)
    {
        var students = await repo.GetAllAsync();
        var data = mapper.Map<List<StudentDto>>(students);

        return Results.Ok(data);
    }

    private static async Task<IResult> GetStudentById(int id, IStudentRepository repo, IMapper mapper)
    {
        var model = await repo.GetAsync(id);

        return model is null ? Results.NotFound() : Results.Ok(mapper.Map<StudentDto>(model));
    }

    private static async Task<IResult> UpdateStudent(int id, StudentDto studentDto, IStudentRepository repo,
        IMapper mapper)
    {
        var foundModel = await repo.GetAsync(id);

        if (foundModel is null) return Results.NotFound();

        mapper.Map(studentDto, foundModel);

        await repo.UpdateAsync(foundModel);

        return Results.NoContent();
    }

    private static async Task<IResult> CreateStudent(CreateStudentDto studentDto, IStudentRepository repo,
        IMapper mapper)
    {
        var student = mapper.Map<Student>(studentDto);

        await repo.AddAsync(student);

        return Results.Created($"/Students/{student.Id}", student);
    }

    private static async Task<IResult> DeleteStudent(int id, IStudentRepository repo)
    {
        return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
    }
}