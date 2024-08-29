using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project1.Data;
using Project1.Models;
using Project1.Models.Enums;
using WorkAllocationApp.Models.Dtos;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllocationController : ControllerBase
    {

        private readonly AllocationDbContext _context;

        private readonly ILogger<AllocationController> _logger;
        private List<Course> _courses = new List<Course>();
        private List<Lecturer> _lecturers = new List<Lecturer>();

        public AllocationController(AllocationDbContext context, ILogger<AllocationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("courses")]
        [HttpGet]
        public async Task<IEnumerable<Course>> GetStudentsAsync()
        {
            _courses = await _context.Courses.ToListAsync();
            return _courses;
        }

        [Route("lecturers")]
        [HttpGet]
        public async Task<IEnumerable<Lecturer>> GetLecturersAsync()
        {
            _lecturers = await _context.Lecturers.ToListAsync();
            foreach (var lecturer in _lecturers)
            {
                lecturer.MinTeachingHrs = _context.MinTeachingHoursByRole.FirstOrDefault(x => x.Role == lecturer.Role)?.MinNoOfHours ?? 0;
            }
            return _lecturers;
        }

        [Route("initial-allocations")]
        [HttpGet]
        public async Task<IActionResult> GetInitialAllocationsAsync()
        {
            _courses = await _context.Courses.ToListAsync();
            _lecturers = await _context.Lecturers.ToListAsync();

            var allocations = InitiateAllocations();
            //return Ok(allocations);
            var json = JsonConvert.SerializeObject(allocations);
            return Ok(json);
        }

        [Route("save")]
        [HttpPost]
        public async Task<IActionResult> SaveAllocationsAsync([FromBody] AllocationCell[][] allocations)
        {
            foreach (var row in allocations)
            {
                // Filter out cells with CreditsAllocation = 0
                var validAllocations = row.Where(cell => cell.CreditsAllocation > 0).ToList();

                if (validAllocations.Any())
                {
                    await _context.AllocationCells.AddRangeAsync(validAllocations);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private AllocationCell[,] InitiateAllocations()
        {
            var allocations = new AllocationCell[_courses.Count, _lecturers.Count];
            var planId = Guid.NewGuid();
            for (var courseIndex = 0; courseIndex < _courses.Count; courseIndex++)
            {
                for (var lecturerIndex = 0; lecturerIndex < _lecturers.Count; lecturerIndex++)
                {
                    var cellData = new AllocationCell
                    {
                        CourseId = _courses[courseIndex].CourseId,
                        CourseName = _courses[courseIndex].Name,
                        LecturerId = _lecturers[lecturerIndex].LecturerId,
                        AllocationPlanId = planId
                    };
                    allocations[courseIndex, lecturerIndex] = cellData;
                }
            }
            return allocations;
        }

        [HttpGet("hours")]
        public async Task<ActionResult<IEnumerable<HoursPerCreditForActivity>>> GetHoursPerCreditForActivity()
        {
            return await _context.HoursPerCreditForActivity.ToListAsync();
        }

        [Route("view-allocation")]
        [HttpGet]
        public async Task<IActionResult> GetGroupedAllocations()
        {
            var rawAllocations = await _context.AllocationCells
                .Join(_context.Lecturers,
                    allocation => allocation.LecturerId,
                    lecturer => lecturer.LecturerId,
                    (allocation, lecturer) => new
                    {
                        LecturerId = lecturer.LecturerId,
                        LecturerName = lecturer.Name,
                        CourseCode = allocation.CourseName,
                        LectureHours = allocation.LectureHours,
                        TutorialHours = allocation.TutorialHours,
                        DiscussionHours = allocation.DiscussionHours
                    })
                .ToListAsync();

            // Transform the raw data into the desired DTO format
            var lecturerAllocations = rawAllocations
                .GroupBy(a => new { a.LecturerId, a.LecturerName })
                .Select(g => new LecturerAllocationDto
                {
                    LecturerId = (g.Key.LecturerId).ToString(),
                    LecturerName = g.Key.LecturerName,
                    Allocations = g.Select(a => new List<AllocationDto>
                    {
                new AllocationDto
                {
                    CourseCode = a.CourseCode,
                    Activity = "Lecture Hours",
                    HoursSpent = a.LectureHours
                },
                new AllocationDto
                {
                    CourseCode = a.CourseCode,
                    Activity = "Tutorial Hours",
                    HoursSpent = a.TutorialHours
                },
                new AllocationDto
                {
                    CourseCode = a.CourseCode,
                    Activity = "Discussion Hours",
                    HoursSpent = a.DiscussionHours
                }
                    }).SelectMany(x => x).Where(a => a.HoursSpent > 0).ToList()
                }).ToList();

            return Ok(lecturerAllocations);
        }

        [HttpGet("get-allocations/{lecturerId}")]
        public async Task<IActionResult> GetAllocationsForLecturer(string lecturerId)
        {
            var allocations = await _context.AllocationCells
                .Where(ac => ac.LecturerId.ToString() == lecturerId)
                .ToListAsync();

            return Ok(allocations);
        }

    }
}