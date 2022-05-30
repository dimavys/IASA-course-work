using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelTask
	{
		private int id;

		public int IdBuilder { get { return id; } set { id = value; } }

		private string title;

		public string TitleBuilder { get { return title; } set { title = value; } }

		private string description;

		public string DescBuilder { get { return description; } set { description = value; } }

		private DateTime startDate;

		public DateTime StartBuilder { get { return startDate; } set { startDate = value; } }

		private DateTime finishDate { get; set; }

		public DateTime FinishBuilder { get { return finishDate; } set { finishDate = value; } }

		private int priority;

		public int PriorityBuilder { get { return priority; } set { priority = value; } }

		public void CopyTask(Data.Task t, int repId)
		{
			t.Title = title;
			t.Description = description;
			t.StartDate = startDate;
			t.FinishDate = finishDate;
			t.Priority = priority;
			t.RepositoryId = repId;
		}
	}
}

