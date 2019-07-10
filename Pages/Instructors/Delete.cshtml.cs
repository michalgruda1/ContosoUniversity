﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Instructors
{
	public class DeleteModel : PageModel
	{
		private readonly ContosoUniversity.Models.SchoolContext _context;

		public DeleteModel(ContosoUniversity.Models.SchoolContext context)
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

			Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

			if (Instructor == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Instructor = await _context.Instructors
				.Include(i => i.CourseAssignments)
				.SingleAsync(i => i.ID == id);

			if (Instructor != null)
			{
				var departments = _context.Departments
					.Where(d => d.InstructorID == id)
					.ForEachAsync(d => d.InstructorID = null);
				_context.Instructors.Remove(Instructor);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
