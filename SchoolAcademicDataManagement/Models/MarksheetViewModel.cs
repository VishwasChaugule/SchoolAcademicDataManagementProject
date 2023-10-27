using System;
namespace SchoolAcademicDataManagement.Models
{
	public class MarksheetViewModel
	{
        public int StudentID { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public int ClassID { get; set; }
        public List<MarkViewModel> Marks { get; set; }
        public decimal OverallPercentage { get; set; }
    }
}

