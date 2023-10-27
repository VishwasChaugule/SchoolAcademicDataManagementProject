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
                .ToList();

            if (marksheet == null || marksheet.Count == 0)
            {
                return NotFound("Marksheet not found for the given student and class.");
            }

            return Ok(marksheet);
        }
    }
}

