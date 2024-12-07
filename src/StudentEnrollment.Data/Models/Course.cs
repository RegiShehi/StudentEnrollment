namespace StudentEnrollment.Data.Models;

public class Course : BaseEntity
{
    public required string Title { get; set; }
    public int Credits { get; set; }

    public List<Enrollment> Enrollments { get; set; } = [];
}