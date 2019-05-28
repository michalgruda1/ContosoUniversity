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
		//overposting protection version 1
		//public Student Student { get; set; }

		//overposting protection version 2
		public StudentViewModel StudentVM { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			//overposting protection version 1
			//var emptyStudent = new Student();
			//if (await TryUpdateModelAsync<Student>(
			//	emptyStudent,
			//	"student",
			//	s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
			//{
			//	_context.Student.Add(emptyStudent);
			//	await _context.SaveChangesAsync();
			//	return RedirectToPage("./Index");
			//};
			//return null;

			//overposting protection version 2
			var newStudent = _context.Add(new Student());
			newStudent.CurrentValues.SetValues(StudentVM);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
			
		}
	}
}