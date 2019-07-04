using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Courses
{
	public class IndexModel : PageModel
	{
		private readonly ContosoUniversity.Models.SchoolContext _context;

		public IndexModel(ContosoUniversity.Models.SchoolContext context)
		{
			_context = context;
		}

		public IList<CourseViewModel> Course { get; set; }

		public async Task OnGetAsync()
		{
			Course = await _context.Course
				.Select(c => new CourseViewModel
				{
					CourseID = c.CourseID,
					Title = c.Title,
					Credits = c.Credits,
					DepartmentName = c.Department.Name,
				})
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
