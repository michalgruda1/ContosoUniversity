using System.Collections.Generic;

namespace ContosoUniversity.Models
{
	public class CourseAssignment
	{
		public HashSet<CourseAssignment> CourseAssignments { get; set; }
	}
}