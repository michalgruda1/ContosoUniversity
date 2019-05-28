using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Students
{
	public class DetailsModel : PageModel
	{
		private readonly ContosoUniversity.Models.SchoolContext _context;

		public DetailsModel(ContosoUniversity.Models.SchoolContext context)
		{
			_context = context;
		}

		public Student Student { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Student = await _context.Student.FindAsync(id);

			if (Student == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
