using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelTask
	{
		private int id;

		private string title;

		private string description;

		private DateTime startDate;

		private DateTime finishDate;

		private int priority;

		public ModelTask()
		{
			title = "default";
			description = "default";
			priority = -1;
		}

		public ModelTask(int _id, string _title, string _desc, DateTime _st, DateTime _fn, int _priority)
		{
			id = _id;
			title = _title;
			description = _desc;
			startDate = _st;
			finishDate = _fn;
			priority = _priority;
		}

		public int IdBuilder { get { return id; } set { id = value; } }

		public string TitleBuilder { get { return title; } set { title = value; } }

		public string DescBuilder { get { return description; } set { description = value; } }

		public DateTime StartBuilder { get { return startDate; } set { startDate = value; } }

		public DateTime FinishBuilder { get { return finishDate; } set { finishDate = value; } }

		public int PriorityBuilder { get { return priority; } set { priority = value; } }

		public void CopyTask(Data.Task t, int repId)
		{
			t.Id = id;
			t.Title = title;
			t.Description = description;
			t.StartDate = startDate;
			t.FinishDate = finishDate;
			t.Priority = priority;
			t.RepositoryId = repId;
		}

		public void CopyTask(Data.Task t)
		{
			t.Id = id;
			t.Title = title;
			t.Description = description;
			t.StartDate = startDate;
			t.FinishDate = finishDate;
			t.Priority = priority;
		}
	}
}

