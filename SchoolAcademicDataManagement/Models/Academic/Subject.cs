using System.ComponentModel.DataAnnotations;

namespace SchoolAcademicDataManagement.Models.Academic
{
    public class Subject
	{
        [Key]
        public int SubjectID { get; set; }

        public string SubjectName { get; set; }
    }
}

