﻿namespace StudentEnrollment.DTOs.Course;

public class CourseDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Credits { get; set; }
}