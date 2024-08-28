namespace WorkAllocationApp.Models.Dtos
{
    public class AllocationDto
    {
        public string CourseCode { get; set; } // The code of the course
        public string Activity { get; set; } // Type of activity (Lecture Hours, Tutorial Hours, etc.)
        public decimal HoursSpent { get; set; } // Number of hours spent on the activity
    }

    public class LecturerAllocationDto
    {
        public string LecturerId { get; set; } // Unique identifier for the lecturer
        public string LecturerName { get; set; } // Name of the lecturer
        public List<AllocationDto> Allocations { get; set; } // List of allocations associated with the lecturer
    }

}
