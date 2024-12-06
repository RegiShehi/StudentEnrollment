namespace StudentEnrollment.Data;

public class Course : BaseEntity
{
    public required string Title { get; set; }
    public int Credits { get; set; }
}