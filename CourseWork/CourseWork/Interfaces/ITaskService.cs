using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface ITaskService
	{
		List<ModelTask> GetTasks(int repId);
		bool CreateTask(ModelTask mt, int repId);
		ModelTask GetTask(int id);
		bool EditTask(ModelTask mt);
		bool DeleteTask(int id);
	}
}

