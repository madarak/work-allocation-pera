using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public decimal Credits { get; set; }
        public decimal LectureHrs { get; set; }
        public decimal PracticalHrs { get; set; }
        public decimal AssignementHrs { get; set; }
        public decimal TutorialHrs { get; set; }
    }
}