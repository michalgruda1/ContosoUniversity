using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Students
{
	public class CreateModel : PageModel
	{
		private readonly ContosoUniversity.Models.SchoolContext _context;

		public CreateModel(ContosoUniversity.Models.SchoolContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Student Student { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Student.Add(Student);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}