using System;
using SchoolAcademicDataManagement.Models;

namespace SchoolAcademicDataManagement.Data
{
	public class DataSeeder
	{
        private readonly SchoolDBContext _context;

        public DataSeeder(SchoolDBContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            // Seed classes
            var class1 = new Class { ClassName = "Class 1" };
            var class2 = new Class { ClassName = "Class 2" };
            _context.Classes.AddRange(class1, class2);

            // Seed subjects
            var math = new Subject { SubjectName = "Math" };
            var science = new Subject { SubjectName = "Science" };
            _context.Subjects.AddRange(math, science);

            // Seed exams
            var unit1 = new Exam { ExamName = "Unit 1" };
            var unit2 = new Exam { ExamName = "Unit 2" };
            var term1 = new Exam { ExamName = "Term 1" };
            var term2 = new Exam { ExamName = "Term 2" };
            var term3 = new Exam { ExamName = "Term 3" };
            _context.Exams.AddRange(unit1, unit2, term1, term2, term3);

            // Seed students
            var student1 = new Student { RollNumber = "S001", Name = "Alice", Class = class1 };
            var student2 = new Student { RollNumber = "S002", Name = "Bob", Class = class2 };
            _context.Students.AddRange(student1, student2);

            // Seed marks
            var mark1 = new Mark { Student = student1, Subject = math, Exam = unit1, MarksObtained = 90 };
            var mark2 = new Mark { Student = student1, Subject = science, Exam = unit1, MarksObtained = 85 };
            var mark3 = new Mark { Student = student1, Subject = math, Exam = unit2, MarksObtained = 88 };
            var mark4 = new Mark { Student = student1, Subject = science, Exam = unit2, MarksObtained = 82 };
            var mark5 = new Mark { Student = student1, Subject = math, Exam = term1, MarksObtained = 92 };
            var mark6 = new Mark { Student = student1, Subject = science, Exam = term1, MarksObtained = 87 };
            var mark7 = new Mark { Student = student2, Subject = math, Exam = unit1, MarksObtained = 91 };
            var mark8 = new Mark { Student = student2, Subject = science, Exam = unit1, MarksObtained = 86 };
            // ... Add more marks as needed

            _context.Marks.AddRange(mark1, mark2, mark3, mark4, mark5, mark6, mark7, mark8);

            _context.SaveChanges();
        }

    }
}

