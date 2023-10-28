using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAcademicDataManagement.Data;
using SchoolAcademicDataManagement.Models.Academic;

namespace SchoolAcademicDataManagement.Controllers
{
    [ApiController]
    [Route("api/marks")]
    public class MarksController : ControllerBase
    {
        private readonly SchoolDBContext _context;

        public MarksController(SchoolDBContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}/{classId}")]
        public IActionResult GetMarks(int studentId, int classId)
        {
            try
            {
                // Get Marks with student id and class id
                var marksheet = _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .Include(m => m.Exam)
                .Where(m => m.StudentID == studentId && m.Student.ClassID == classId)
                .Select(m => new MarkViewModel
                {
                    SubjectName = m.Subject.SubjectName,
                    ExamName = m.Exam.ExamName,
                    MarksObtained = m.MarksObtained
                })
                .ToList();

                if (marksheet == null || marksheet.Count == 0)
                {
                    return NotFound("Marksheet not found for the given student and class.");
                }

                // Get Student Info
                var studentInfo = _context.Students.FirstOrDefault(s => s.StudentID == studentId);
                // Assign Data to MarksheetViewModel
                var marksheetViewModel = new MarksheetViewModel
                {
                    StudentID = studentId, // Set the student ID
                    RollNumber = studentInfo?.RollNumber,
                    Name = studentInfo?.Name,
                    ClassID = classId, // Set the class ID
                    Marks = marksheet,
                    OverallPercentage = CalculateOverallPercentage(marksheet)
                };

                return Ok(marksheetViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        private decimal CalculateOverallPercentage(List<MarkViewModel> marks)
        {
            if (marks == null || marks.Count == 0)
            {
                return 0; // Return 0 if there are no marks
            }

            decimal totalPercentage = 0;

            // Calculate average percentage for each subject
            foreach (var mark in marks)
            {
                // Assuming MarksObtained is the percentage for each subject
                totalPercentage += mark.MarksObtained;
            }

            // Calculate overall average percentage
            decimal overallPercentage = totalPercentage / marks.Count;
            return overallPercentage;
        }
    }
}

