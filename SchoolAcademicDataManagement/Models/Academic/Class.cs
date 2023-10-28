using System.ComponentModel.DataAnnotations;

namespace SchoolAcademicDataManagement.Models.Academic
{
    public class Class
	{
        [Key]
        public int ClassID { get; set; }

        public string ClassName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}