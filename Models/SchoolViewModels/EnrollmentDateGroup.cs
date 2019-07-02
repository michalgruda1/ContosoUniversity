using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
	public class EnrollmentDateGroup
	{
		[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? EnrollmentDate { get; set; }
		public int StudentCount { get; set; }
	}
}
