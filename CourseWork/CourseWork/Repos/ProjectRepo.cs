using System;
using CourseWork.Data;
using CourseWork.Interfaces;

namespace CourseWork.Repos
{
	public class ProjectRepo : BaseRepo<Project>, IProjectRepo
	{
		public ProjectRepo(AppDbContext appDbContext) :base(appDbContext)
		{

		}

		public List<Project> GetProjects()
        {
			var tmp = _appDbContext.Projects.ToList();
			return tmp;
		}

		public Project GetProjectAsLeader(int lid)
		{
			var tmp = (from p in _appDbContext.Projects
					   join t in _appDbContext.Teams
					   on p.TeamId equals t.Id
					   where t.TeamLeadId == lid
					   select p).FirstOrDefault();
			return tmp;
		}

		public Project GetProjectAsCustomer(int cid)
		{
			var tmp = (from p in _appDbContext.Projects
					   join t in _appDbContext.Teams
					   on p.TeamId equals t.Id
					   where t.CustomerId == cid
					   select p).FirstOrDefault();
			return tmp;
		}

		public void CreateProject(Project p)
		{
			_appDbContext.Projects.Add(p);
			_appDbContext.SaveChanges();
		}

		public Project FetchProjectByName(string pname)
        {
			var tmp = _appDbContext.Projects.Where(x => x.Name == pname).FirstOrDefault();
			return tmp;
        }

		public Project FetchProjectById(int pId)
		{
			var tmp = _appDbContext.Projects.Where(x => x.Id == pId).FirstOrDefault();
			return tmp;
		}

		public Project FetchProjectByLeader(int lid)
		{
			var tmp = (from t in _appDbContext.Teams
					   join p in _appDbContext.Projects
					   on t.Id equals p.TeamId
					   where t.TeamLeadId == lid
					   select p).FirstOrDefault();
			return tmp;
		}

		public Project FetchProjectByTeam(string teamName)
		{
			var tmp = (from t in _appDbContext.Teams
					   join p in _appDbContext.Projects
					   on t.Id equals p.TeamId
					   where t.Name == teamName
					   select p).FirstOrDefault();
			return tmp;
		}

		public void DeleteProject(Project p)
		{
			_appDbContext.Projects.Remove(p);
			_appDbContext.SaveChanges();
		}

		public List<Project> GetProjectsAsWorker(int wId)
		{
			var tmp = (from u in _appDbContext.Users
					   join wks in _appDbContext.Workings
					   on u.Id equals wks.WorkerId
					   join t in _appDbContext.Teams
					   on wks.TeamId equals t.Id
					   join p in _appDbContext.Projects
					   on t.Id equals p.TeamId
					   where u.Id == wId
					   select p).ToList();
			return tmp;
		}

		public void EditProject(Project p)
		{
			var tmp = FetchProjectById(p.Id);
			tmp.Name = p.Name;
			tmp.Regulations = p.Regulations;
			tmp.Description = p.Description;
			tmp.Status = p.Status;
			_appDbContext.SaveChanges();
		}
	}
}

