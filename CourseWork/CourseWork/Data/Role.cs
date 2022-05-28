using System;
namespace CourseWork.Data
{
	public class Role
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string NormalizedName { get; set; }

		public IEnumerable<User> Users;
	}
}

