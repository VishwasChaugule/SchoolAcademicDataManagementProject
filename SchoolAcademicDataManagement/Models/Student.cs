using System;
namespace SchoolAcademicDataManagement.Models
{
	public class Student
	{
        public int StudentID { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public int ClassID { get; set; }
        public Class Class { get; set; }
    }
}

