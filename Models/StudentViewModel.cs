using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
	public class StudentViewModel
	{
		public int ID { get; set; }
		public string LastName { get; set; }
		public string FirstMidName { get; set; }
		public DateTime EnrollmentDate { get; set; }

		public HashSet<Enrollment> Enrollments { get; set; }
	}
}
