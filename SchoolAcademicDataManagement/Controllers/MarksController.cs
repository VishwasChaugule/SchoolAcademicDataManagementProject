using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAcademicDataManagement.Data;
using SchoolAcademicDataManagement.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            var marksheetViewModel = new MarksheetViewModel
            {
                StudentID = studentId, // Set the student ID
                RollNumber = _context.Students.FirstOrDefault(s => s.StudentID == studentId)?.RollNumber,
                Name = _context.Students.FirstOrDefault(s => s.StudentID == studentId)?.Name,
                ClassID = classId, // Set the class ID
                Marks = marksheet,
                OverallPercentage = CalculateOverallPercentage(marksheet)
            };

            return Ok(marksheetViewModel);
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

