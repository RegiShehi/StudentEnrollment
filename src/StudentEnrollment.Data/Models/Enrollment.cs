﻿namespace StudentEnrollment.Data.Models;

public class Enrollment : BaseEntity
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public Course? Course { get; set; }
    public Student? Student { get; set; }
}