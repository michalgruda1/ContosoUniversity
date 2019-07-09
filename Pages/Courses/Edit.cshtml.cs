﻿using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Courses
{
	public class EditModel : DepartmentNamePageModel
	{
		private readonly SchoolContext _context;

		public EditModel(SchoolContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Course Course { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				PopulateDepartmentsDropDownList(_context);
				return NotFound();
			}

			Course = await _context.Course
					.Include(c => c.Department).SingleOrDefaultAsync(m => m.CourseID == id);

			if (Course == null)
			{
				return NotFound();
			}

			// Select current DepartmentID.
			PopulateDepartmentsDropDownList(_context, Course.DepartmentID);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var courseToUpdate = await _context.Course.FindAsync(id);

			if (await TryUpdateModelAsync<Course>(
					 courseToUpdate,
					 nameof(Models.Course),   // Prefix for form value.
						 c => c.Credits, c => c.DepartmentID, c => c.Title))
			{
				await _context.SaveChangesAsync();
				return RedirectToPage("./Index");
			}

			// Select DepartmentID if TryUpdateModelAsync fails.
			PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentID);
			return Page();
		}
	}
}