﻿using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Instructors
{
	public class EditModel : PageModel
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
					.AsNoTracking()
					.FirstOrDefaultAsync(m => m.ID == id);

			if (Instructor == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var instructorToUpdate = await _context.Instructors
					.Include(i => i.OfficeAssignment)
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
				await _context.SaveChangesAsync();
			}
			return RedirectToPage("./Index");

		}
	}
}