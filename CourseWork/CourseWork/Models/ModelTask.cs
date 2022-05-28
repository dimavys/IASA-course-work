using System;
namespace CourseWork.Models
{
	public class ModelTask
	{
		private int id { get; }

		private string title { get; set; }

		private string description { get; set; }

		private DateTime startDate { get; set; }

		private DateTime finishDate { get; set; }

		private int priority { get; set; }

		private int repositoryId { get; }
	}
}

