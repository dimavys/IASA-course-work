using System;
namespace CourseWork.Data
{
	public class User
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int RoleId { get; set; }

		public double? Salary { get; set; }

		public IEnumerable<Working> Workings { get; set; }

	}
}

