using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IProjectService
	{
		List<ModelProject> SeeProjects();
		bool CreateProject(ModelProject mp);
		bool DeleteProject(int pid);
		ModelProject SeeProjectAsLeader(int lId);
		ModelProject SeeProjectAsCustomer(int cId);
		List<ModelProject> SeeProjectsAsWorker(int wId);
		ModelProject GetProject(int pId);
		bool EditProject(ModelProject mp);
	}
}

