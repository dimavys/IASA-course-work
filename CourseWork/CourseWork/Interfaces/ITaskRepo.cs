using System;
namespace CourseWork.Interfaces
{
	public interface ITaskRepo
	{
		List<Data.Task> GetTasks(int repId);
		void CreateTask(Data.Task t);
		Data.Task FetchTaskByName(string name, int repId);
		Data.Task FetchTaskById(int id);
		void EditTask(Data.Task t);
		void DeleteTask(Data.Task t);
	}
}

