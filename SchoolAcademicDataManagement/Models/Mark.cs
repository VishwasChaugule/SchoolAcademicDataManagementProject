using System;
namespace SchoolAcademicDataManagement.Models
{
	public class Mark
	{
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public int MarksObtained { get; set; }
    }
}

