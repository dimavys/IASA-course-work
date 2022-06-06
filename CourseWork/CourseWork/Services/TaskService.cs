using System;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepo _taskREpo;

		public TaskService(ITaskRepo taskRepo)
		{
			_taskREpo = taskRepo;
		}

		public List<ModelTask> GetTasks(int repId)
        {
			var tmp = _taskREpo.GetTasks(repId);			
			var list = new List<ModelTask>();
			foreach (var r in tmp)
					list.Add(new ModelTask(r.Id, r.Title,r.Description, r.StartDate,r.FinishDate,r.Priority));
			return list;
        }

		public bool CreateTask(ModelTask mt, int repId)
        {
			if (mt.FinishBuilder > mt.StartBuilder)
            {
				if (_taskREpo.FetchTaskByName(mt.TitleBuilder, repId) == null )
                {
					var task = new Data.Task();
					mt.CopyTask(task, repId);
					_taskREpo.CreateTask(task);
					return true;
				}
            }
			return false;
        }

		public bool EditTask(ModelTask mt)
        {
			var tmp = _taskREpo.FetchTaskById(mt.IdBuilder);
			if (tmp != null)
			{
				var t = new Data.Task();
				mt.CopyTask(t);
				_taskREpo.EditTask(t);
				return true;
			}
			return false;
        }

		public ModelTask GetTask(int id)
        {
			var tmp = _taskREpo.FetchTaskById(id);
			var mt = new ModelTask(tmp.Id, tmp.Title, tmp.Description, tmp.StartDate, tmp.FinishDate, tmp.Priority);
			return mt;
        }

		public bool DeleteTask(int id)
        {
			var tmp = _taskREpo.FetchTaskById(id);
			if (tmp != null)
            {
				_taskREpo.DeleteTask(tmp);
				return true;
            }
			return false;
        }
	}
}

