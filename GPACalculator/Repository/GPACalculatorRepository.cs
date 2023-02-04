using GPACalculator.Data;
using GPACalculator.Entities;
using GPACalculator.Models;
using GPACalculator.Service;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.Repository {
    public interface IGPACalculatorRepository {
        public Task<Student> StudentRegisterAsync(StudentsRegisterRequest request);
        public Task<Subject> SubjectRegisterAsync(SubjectsRegisterRequest request);
        public Task<Grade> GradeRegisterAsync(int studentId, GradesRegisterRequest request);
        public Task<List<Grade>> ReturnGradesAsync(int StudentId);
        public Task<double> ReturnGPAAsync(int studentId);

    }
    public class GPACalculatorRepository : IGPACalculatorRepository 
    {
        private readonly AppDbContext _db;
        public GPACalculatorRepository(AppDbContext db) {
            _db = db;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Identity { get; set; }
        public int Year { get; set; }
        public async Task<Student> StudentRegisterAsync(StudentsRegisterRequest request) {
            var student = new Student();
            student = await _db.Students.Where(x => x.FirstName.Equals(request.FirstName))
                                .Where(x => x.LastName.Equals(request.LastName))
                                .Where(x => x.CourseName.Equals(request.CourseName))
                                .Where(x => x.PrivateNumber.Equals(request.CourseName))
                                .FirstOrDefaultAsync();
            if (student != null) return null;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.PrivateNumber = request.PrivateNumber;
            student.CourseName = request.CourseName;
            await _db.Students.AddAsync(student);
            return student;
        }
        public async Task<Subject> SubjectRegisterAsync(SubjectsRegisterRequest request)
        {
            var subject = new Subject();
            subject = await _db.Subjects.Where(x => x.Name.Equals(request.Name))
                                .FirstOrDefaultAsync();
            if (subject != null) return null;
            subject.Name = request.Name;
            subject.Credits = request.Credits;
            await _db.Subjects.AddAsync(subject);
            return subject;
        }
        public async Task<Grade> GradeRegisterAsync(int StudentId, GradesRegisterRequest request) {
            var grade = new Grade();
            grade = await _db.Grades.Where(x => x.StudentId.Equals(StudentId))
                                .Where(x => x.SubjectId.Equals(request.SubjectId))
                                .Where(x => x.Score.Equals(request.Score))
                                .FirstOrDefaultAsync();
            if (grade != null) return null;
            grade.StudentId =StudentId;
            grade.SubjectId = request.SubjectId;
            grade.Score=request.Score;
            await _db.Grades.AddAsync(grade);
            return grade;
        }
        public async Task<List<Grade>> ReturnGradesAsync(int StudentId) {
            var gradesList = await _db.Grades.Where(x => x.StudentId.Equals(StudentId)).ToListAsync();
            return gradesList;
        }
        public async Task<double> ReturnGPAAsync(int studentId) {
            var list = await ReturnGradesAsync(studentId);
            return await Calculator.CalculateGPA(list, _db);
        }
             
    }
}
