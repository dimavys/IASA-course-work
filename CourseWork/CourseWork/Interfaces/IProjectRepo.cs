using System;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IProjectRepo
	{
		List<Project> GetProjects();
		void CreateProject(Project p);
		Project FetchProjectByName(string pname);
		Project FetchProjectByLeader(int lid);
		Project FetchProjectById(int pId);
		Project FetchProjectByTeam(string teamName);
		void DeleteProject(Project p);
		Project GetProjectAsLeader(int lid);
		Project GetProjectAsCustomer(int cid);
		List<Project> GetProjectsAsWorker(int wId);
		void EditProject(Project p);
	}
}

