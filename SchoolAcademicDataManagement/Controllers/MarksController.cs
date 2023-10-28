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

        [HttpGet("{rollNumber}/{classId}")]
        public IActionResult GetMarks(string rollNumber, int classId)
        {
            try
            {
                // Get Student Info
                var studentInfo = GetStudentInfoByRollNumber(rollNumber);
                if (studentInfo == null)
                {
                    return NotFound("Student not found for the given student roll number and class id.");
                }

                // Get Marks with student id and class id
                var marksheet = GetMarksByStudentIdAndClassId(studentInfo.StudentID, classId);
                if (marksheet == null || marksheet.Count == 0)
                {
                    return NotFound("Marksheet not found for the given student and class.");
                }

                // Assign Data to MarksheetViewModel
                var marksheetViewModel = new MarksheetViewModel
                {
                    StudentID = studentInfo.StudentID, // Set the student ID
                    RollNumber = studentInfo.RollNumber, // Set the Roll number
                    Name = studentInfo.Name, // Set the student Name
                    ClassID = classId, // Set the class ID
                    Marks = marksheet, // Set Marksheet
                    OverallPercentage = CalculateOverallPercentage(marksheet) // Set Percentage
                };

                return Ok(marksheetViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        private Student GetStudentInfoByRollNumber(string rollNumber)
        {
            return _context.Students.FirstOrDefault(s => s.RollNumber == rollNumber);
        }

        private List<MarkViewModel> GetMarksByStudentIdAndClassId(int studentId, int classId)
        {
            return _context.Marks
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

