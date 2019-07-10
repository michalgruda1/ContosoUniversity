using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Pages.Courses
{
	public class CourseViewModel
	{
		[Display(Name = "Number")]
		public int CourseID { get; set; }
		public string Title { get; set; }
		public int Credits { get; set; }
		[Display(Name = "Department name")]
		public string DepartmentName { get; set; }
	}
}