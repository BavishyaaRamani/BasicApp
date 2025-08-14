using System;
using System.ComponentModel.DataAnnotations;

namespace BasicApp.Models
{
	public class Employee
	{
		[Key]
		public Guid Id {get; set;}
		public string Name { get; set; }
		public string MobileNo { get; set; }
		public string EmailID { get; set; }
	}
}

