using System.Collections.Generic;

namespace ContosoUniversity.Models
{
	public class CourseAssignment
	{
		public virtual int InstructorID { get; set; }
		public virtual int CourseID { get; set; }
		public virtual Instructor Instructor { get; set; }
		public virtual Course Course { get; set; }
	}
}