using System;
namespace SchoolAcademicDataManagement.Models
{
	public class Class
	{
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}

