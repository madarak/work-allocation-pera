using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class AllocationCell
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AllocationCellId { get; set; }
        public Guid AllocationPlanId { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public Guid LecturerId { get; set; }
        public decimal CreditsAllocation { get; set; }
        public decimal LectureHours { get; set; }
        public decimal TutorialHours { get; set; }
        public decimal LabHours { get; set; }
        public decimal DemonstrationHours { get; set; }
        public decimal ClinicalHours { get; set; }
        public decimal DiscussionHours { get; set; }
        public decimal FieldworkHours { get; set; }
    }
}