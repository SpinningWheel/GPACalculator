using GPACalculator.Data;
using GPACalculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.Service {
    public class Calculator {
        public static async Task<double> CalculateGPA(List<Grade> list, AppDbContext db) {
            double result = 0; double resultDivisor = 0;
            if (list.Count == 0) return -1;
            foreach (Grade grade in list) {
                var subject = await db.Subjects.Where(x => x.Id.Equals(grade.SubjectId)).FirstOrDefaultAsync();
                result += (grade.Score * subject.Credits);
                resultDivisor += subject.Credits;
            }
            return result / resultDivisor;
        }
    }
}

