using AutoMapper;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Models;
using StudentEnrollment.DTOs.Enrollment;

namespace StudentEnrollment.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Enrollment", async (IEnrollmentRepository repo, IMapper mapper) =>
            {
                var enrollments = await repo.GetAllAsync();
                var data = mapper.Map<List<EnrollmentDto>>(enrollments);
                return data;
            })
            .WithTags(nameof(Enrollment))
            .WithName("GetAllEnrollments")
            .Produces<List<EnrollmentDto>>();

        routes.MapGet("/api/Enrollment/{id}", async (int id, IEnrollmentRepository repo, IMapper mapper) =>
            {
                var model = await repo.GetAsync(id);

                return model is null ? Results.NotFound() : Results.Ok(mapper.Map<EnrollmentDto>(model));
            })
            .WithTags(nameof(Enrollment))
            .WithName("GetEnrollmentById")
            .Produces<EnrollmentDto>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Enrollment/{id}",
                async (int id, EnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
                {
                    var foundModel = await repo.GetAsync(id);

                    if (foundModel is null) return Results.NotFound();
                    //update model properties here
                    mapper.Map(enrollmentDto, foundModel);
                    await repo.UpdateAsync(foundModel);

                    return Results.NoContent();
                })
            .WithTags(nameof(Enrollment))
            .WithName("UpdateEnrollment")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Enrollment/",
                async (CreateEnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
                {
                    var enrollment = mapper.Map<Enrollment>(enrollmentDto);
                    await repo.AddAsync(enrollment);
                    return Results.Created($"/Enrollments/{enrollment.Id}", enrollment);
                })
            .WithTags(nameof(Enrollment))
            .WithName("CreateEnrollment")
            .Produces<Enrollment>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Enrollment/{id}",
                async (int id, IEnrollmentRepository repo) =>
                    await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
            .WithTags(nameof(Enrollment))
            .WithName("DeleteEnrollment")
            .Produces<Enrollment>()
            .Produces(StatusCodes.Status404NotFound);
    }
}