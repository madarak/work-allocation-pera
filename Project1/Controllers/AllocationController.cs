using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project1.Data;
using Project1.Models;

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
			_logger.LogInformation("Saving allocations");
			foreach (var row in allocations)
			{
				await _context.AllocationCells.AddRangeAsync(row);
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
	}
}