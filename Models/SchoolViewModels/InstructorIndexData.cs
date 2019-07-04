using System.Collections.Generic;

namespace ContosoUniversity.Models.SchoolViewModels
{
	public class InstructorIndexData
	{
		public virtual IEnumerable<Instructor> Instructors { get; set; }
		public virtual IEnumerable<Course> Courses { get; set; }
		public virtual IEnumerable<Enrollment> Enrollments { get; set; }
	}
}
