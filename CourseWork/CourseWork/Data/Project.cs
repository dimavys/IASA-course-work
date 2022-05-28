using System;
namespace CourseWork.Data
{
	public class Project
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Regulations { get; set; }

		public string Status { get; set; }

		public int TeamId { get; set; }

		public Team Team { get; set; }

		public IEnumerable<Repository> Repositories;
	}
}

