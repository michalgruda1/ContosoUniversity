using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

		public IList<Instructor> Instructor { get; set; }

		public async Task OnGetAsync()
		{
			Instructor = await _context.Instructors.ToListAsync();
		}
	}
}
