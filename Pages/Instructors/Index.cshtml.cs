﻿using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Instructors
{
	public class IndexModel : PageModel
	{
		private readonly ContosoUniversity.Models.SchoolContext _context;

		public IndexModel(ContosoUniversity.Models.SchoolContext context)
		{
			_context = context;
		}

		public InstructorIndexData Instructor { get; set; }
		public int InstructorID { get; set; }
		public int CourseID { get; set; }

		public async Task OnGetAsync(int? id, int? courseID)
		{
			Instructor = new InstructorIndexData();

			Instructor.Instructors = await _context.Instructors
				.Include(i => i.OfficeAssignment)
				.Include(i => i.CourseAssignments)
					.ThenInclude(i => i.Course)
						.ThenInclude(i => i.Department)
				.AsNoTracking()
				.OrderBy(i => i.LastName)
				.ToListAsync();

			if (id != null)
			{
				InstructorID = id.Value;
				Instructor instructor = Instructor.Instructors
					.Where(i => i.ID == id).Single();
				Instructor.Courses = instructor.CourseAssignments.Select(c => c.Course);
			}

			if (courseID != null)
			{
				CourseID = courseID.Value;
				var selectedCourse = Instructor.Courses
					.Single(c => c.CourseID == courseID.Value);

				await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();

				foreach (var enrollment in selectedCourse.Enrollments)
				{
					await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
				}

				Instructor.Enrollments = selectedCourse.Enrollments;
			}
		}
	}
}
