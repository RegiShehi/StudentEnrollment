namespace StudentEnrollment.DTOs.Course;

public class CreateCourseDto
{
    public required string Title { get; set; }
    public int Credits { get; set; }
}