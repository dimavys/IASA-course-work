using System;
namespace CourseWork.Data
{
	public class Repository
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int ProjectId { get; set; }

		public IEnumerable<Task> Tasks;
	}
}

