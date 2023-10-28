using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAcademicDataManagement.Models.Academic
{
	public class Student
	{
        [Key]
        public int StudentID { get; set; }

        public string RollNumber { get; set; }

        public string Name { get; set; }

        public int ClassID { get; set; }

        [ForeignKey("ClassID")]
        public Class Class { get; set; }
    }
}

