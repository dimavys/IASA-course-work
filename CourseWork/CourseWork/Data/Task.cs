using System;
namespace CourseWork.Data
{
	public class Task
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime FinishDate { get; set; }

		public int Priority { get; set; }

		public int RepositoryId { get; set; }

		public Repository Repo { get; set; }
	}
}

