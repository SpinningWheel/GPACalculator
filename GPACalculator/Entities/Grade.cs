using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;

namespace GPACalculator.Entities {
    public class Grade {
        [Key]           //Validation
        [MaxLength(50)]
        [Required]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }

    }
}
