using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Instructors
{
	public class EditModel : InstructorCoursesPageModel
	{
		private readonly SchoolContext _context;

		public EditModel(SchoolContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Instructor Instructor { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Instructor = await _context.Instructors
					.Include(i => i.OfficeAssignment)
					.Include(i => i.CourseAssignments)
						.ThenInclude(ca => ca.Course)
					.AsNoTracking()
					.FirstOrDefaultAsync(m => m.ID == id);

			if (Instructor == null)
			{
				return NotFound();
			}
			PopulateAssignedCourseData(_context, Instructor);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var instructorToUpdate = await _context.Instructors
					.Include(i => i.OfficeAssignment)
					.Include(i => i.CourseAssignments)
						.ThenInclude(ca => ca.Course)
					.FirstOrDefaultAsync(s => s.ID == id);

			if (await TryUpdateModelAsync<Instructor>(
					instructorToUpdate,
					nameof(Models.Instructor),
					i => i.FirstMidName, i => i.LastName,
					i => i.HireDate, i => i.OfficeAssignment))
			{
				if (String.IsNullOrWhiteSpace(
						instructorToUpdate.OfficeAssignment?.Location))
				{
					instructorToUpdate.OfficeAssignment = null;
				}

				UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
				await _context.SaveChangesAsync();
				return RedirectToPage("./Index");
			}

			UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
			PopulateAssignedCourseData(_context, instructorToUpdate);
			return Page();
		}
	}
}