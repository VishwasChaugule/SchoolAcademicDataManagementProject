using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAcademicDataManagement.Models.Academic
{
    public class Mark
	{
        [Key]
        public int MarkID { get; set; }

        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        public int SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }

        public int ExamID { get; set; }

        [ForeignKey("ExamID")]
        public Exam Exam { get; set; }

        public int MarksObtained { get; set; }
    }
}

