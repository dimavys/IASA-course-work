using System;
using CourseWork.Data;
using CourseWork.Interfaces;

namespace CourseWork.Repos
{
	public class TaskRepo : BaseRepo<Data.Task> , ITaskRepo
	{
		public TaskRepo(AppDbContext appDbContext) : base(appDbContext)
		{
			
		}

		public List<Data.Task> GetTasks(int repId)
        {
			var tmp = _appDbContext.Tasks.Where(x => x.RepositoryId == repId).OrderBy(x => x.Priority).ToList();
			return tmp;
        }

		public void CreateTask (Data.Task t)
        {
			_appDbContext.Tasks.Add(t);
			_appDbContext.SaveChanges();
        }

		public Data.Task FetchTaskByName (string name, int repId)
        {
			var tmp = _appDbContext.Tasks.Where(x => x.Title == name && x.RepositoryId == repId).FirstOrDefault();
			return tmp;
        }

		public Data.Task FetchTaskById(int id)
		{
			var tmp = _appDbContext.Tasks.Where(x => x.Id == id).FirstOrDefault();
			return tmp;
		}

		public void EditTask(Data.Task t)
        {
			var tmp = FetchTaskById(t.Id);
			tmp.Title = t.Title;
			tmp.Description = t.Description;
			tmp.StartDate = t.StartDate;
			tmp.FinishDate = t.FinishDate;
			tmp.Priority = t.Priority;
			_appDbContext.SaveChanges();
		}

		public void DeleteTask(Data.Task t)
        {
			_appDbContext.Tasks.Remove(t);
			_appDbContext.SaveChanges();
        }

	}
}

