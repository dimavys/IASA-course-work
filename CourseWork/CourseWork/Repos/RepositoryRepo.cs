using System;
using CourseWork.Data;
using CourseWork.Interfaces;

namespace CourseWork.Repos
{
	public class RepositoryRepo : BaseRepo<Repository>, IRepositoryRepo
	{
		public RepositoryRepo(AppDbContext appDbContext) : base(appDbContext)
		{

		}

		public List<Repository> GetRepositoriesAsLeader(int lid)
        {
			var tmp = (from t in _appDbContext.Teams
					   join p in _appDbContext.Projects
					   on t.Id equals p.TeamId
					   join r in _appDbContext.Repositories
					   on p.Id equals r.ProjectId
					   where t.TeamLeadId == lid
					   select r).ToList();
			return tmp;
		}

		public List<Repository> GetRepositoriesAsWorker(int pid)
        {
			var tmp = (from r in _appDbContext.Repositories
					   join p in _appDbContext.Projects
					   on r.ProjectId equals p.Id
					   where p.Id == pid
					   select r).ToList();
			return tmp;
		}

		public Repository FetchRepositoryById(int id)
		{
			var tmp = _appDbContext.Repositories.Where(x => x.Id == id).FirstOrDefault();
			return tmp;
		}

		public Repository FetchRepositoryByName(string name)
		{ 
			var tmp = _appDbContext.Repositories.Where(x => x.Name == name).FirstOrDefault();
			return tmp;
		}

		public void DeleteRepository(Repository r)
        {
			_appDbContext.Repositories.Remove(r);
			_appDbContext.SaveChanges();
        }

		public void CreateRepository(Repository r)
        {
			_appDbContext.Repositories.Add(r);
			_appDbContext.SaveChanges();
        }

		public void EditRepository (Repository r)
        {
			var tmp = FetchRepositoryById(r.Id);
			tmp.Name = r.Name;
			_appDbContext.SaveChanges();
        }
	}
}

