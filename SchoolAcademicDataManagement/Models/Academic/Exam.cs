using System.ComponentModel.DataAnnotations;

namespace SchoolAcademicDataManagement.Models.Academic
{
	public class Exam
	{
        [Key]
        public int ExamID { get; set; }

        public string ExamName { get; set; }
    }
}