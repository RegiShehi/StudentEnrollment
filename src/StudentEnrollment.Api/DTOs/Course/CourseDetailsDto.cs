﻿using StudentEnrollment.DTOs.Student;

namespace StudentEnrollment.DTOs.Course;

public class CourseDetailsDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Credits { get; set; }

    public List<StudentDto> Students { get; set; } = [];
}