using System;
namespace CourseWork.Data
{
	public class Team
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Rating { get; set; }

		public int TeamLeadId { get; set; }

		public int CustomerId { get; set; }

		public User Customer { get; set; }

		public User TeamLeader { get; set; }

		public IEnumerable<Working> Workings;
	}
}

