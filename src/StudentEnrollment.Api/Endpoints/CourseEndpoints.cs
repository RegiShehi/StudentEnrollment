using AutoMapper;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Models;
using StudentEnrollment.DTOs.Course;

namespace StudentEnrollment.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Course", async (ICourseRepository repo, IMapper mapper) =>
            {
                var courses = await repo.GetAllAsync();
                var data = mapper.Map<List<CourseDto>>(courses);
                return data;
            })
            .WithTags(nameof(Course))
            .WithName("GetAllCourses")
            .Produces<List<CourseDto>>();

        routes.MapGet("/api/Course/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
            {
                var model = await repo.GetAsync(id);

                return model is null ? Results.NotFound() : Results.Ok(mapper.Map<CourseDto>(model));
            })
            .WithTags(nameof(Course))
            .WithName("GetCourseById")
            .Produces<CourseDto>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapGet("/api/Course/GetStudents/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
            {
                var model = await repo.GetAsync(id);

                return model is null ? Results.NotFound() : Results.Ok(mapper.Map<CourseDetailsDto>(model));
            })
            .WithTags(nameof(Course))
            .WithName("GetCourseDetailsById")
            .Produces<CourseDetailsDto>()
            .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Course/{id}", async (int id, CourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
            {
                var foundModel = await repo.GetAsync(id);

                if (foundModel is null) return Results.NotFound();
                //update model properties here
                mapper.Map(courseDto, foundModel);
                await repo.UpdateAsync(foundModel);

                return Results.NoContent();
            })
            .WithTags(nameof(Course))
            .WithName("UpdateCourse")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Course/", async (CreateCourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
            {
                var course = mapper.Map<Course>(courseDto);
                await repo.AddAsync(course);
                return Results.Created($"/Courses/{course.Id}", course);
            })
            .WithTags(nameof(Course))
            .WithName("CreateCourse")
            .Produces<Course>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Course/{id}",
                async (int id, ICourseRepository repo) =>
                    await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
            .WithTags(nameof(Course))
            .WithName("DeleteCourse")
            .Produces<Course>()
            .Produces(StatusCodes.Status404NotFound);
    }
}