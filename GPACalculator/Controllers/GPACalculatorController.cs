using GPACalculator.Entities;
using GPACalculator.Models;
using GPACalculator.Repository;
using Microsoft.AspNetCore.Mvc;
namespace GPACalculator.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class GPACalculatorController : ControllerBase {

        private IGPACalculatorRepository _gcr;

        public GPACalculatorController(IGPACalculatorRepository gcr) {
            _gcr = gcr;    
        }

        [HttpPost]
        [Route("Post/Students")]
        public async /*void for async*/ Task<IActionResult> RegisterStudents([FromBody] StudentsRegisterRequest request) {
            var student = await _gcr.StudentRegisterAsync(request);
            if (student == null) return BadRequest("Something Is Wrong");
            return Ok("Das Ist Fantastish " + student.PrivateNumber);

        }
        [HttpPost]
        [Route("Post/Subjects")]
        public async Task<IActionResult> RegisterSubjects ([FromBody] SubjectsRegisterRequest request) {
            var subject = await _gcr.SubjectRegisterAsync(request);
            if (subject == null) return BadRequest("Something Is Wrong");
            return Ok("Das Ist Fantastish " + subject.Id);
        }
        [HttpPost]
        [Route("POST / students /{studentId}/grades")]
        public async Task<IActionResult> RegisterGrades (int studentId,[FromBody] GradesRegisterRequest request) {
            var grade = await _gcr.GradeRegisterAsync(studentId, request);
            if (grade == null) return BadRequest("Something Is Wrong");
            return Ok("Das Ist Fantastish " + grade.Id);
        }

        [HttpGet]
        [Route("GET /students/{studentId}/grades")]
        public async Task<IActionResult> ReturnGrades (int studentId) {
            var list = await _gcr.ReturnGradesAsync(studentId);
            if(list==null) return BadRequest("Something Is Wrong");
            return Ok("Das Ist Fantastish " + list.Count);
        }
        [HttpGet]
        [Route("GET /students/{studentId}/gpa")]
        public async Task<IActionResult> ReturnGPA (int studentId) {
            var result= await _gcr.ReturnGPAAsync(studentId);
            if(result==-1) return BadRequest("Something Is Wrong");
            return Ok("Das Ist Fantastish " + result);
        }

    }
}
