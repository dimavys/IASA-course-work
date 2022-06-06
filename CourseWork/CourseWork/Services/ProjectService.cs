using System;
using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class ProjectService : IProjectService
	{
		private readonly ITeamRepo _teamRepo;
		private readonly IUserRepo _userRepo;
		private readonly IProjectRepo _projectRepo;

		public ProjectService(ITeamRepo teamRepo, IUserRepo userRepo, IProjectRepo projectRepo)
		{
			_teamRepo = teamRepo;
			_userRepo = userRepo;
			_projectRepo = projectRepo;
		}

		public List<ModelProject> SeeProjects()
        {
			var projects = _projectRepo.GetProjects();
			if (projects.FirstOrDefault() != null)
			{
				List<ModelProject> list = new List<ModelProject>();
				foreach (var r in projects)
					list.Add(new ModelProject(r.Id, r.Name, _teamRepo.FetchTeamById(r.TeamId).Name, r.Description, r.Regulations, r.Status));
				return list;
			}
			else
				return null;
        }

		public bool CreateProject(ModelProject mp)
        {
			if (_projectRepo.FetchProjectByName(mp.NameBuilder) != null)
				return false;
			else if (_projectRepo.FetchProjectByTeam(mp.TeamBuilder) != null)
				return false;
			else if (_teamRepo.FetchTeamByName(mp.TeamBuilder) != null)
			{
				var tmp = new Project();
				var team = _teamRepo.FetchTeamByName(mp.TeamBuilder);
				mp.CopyProject(tmp, team.Id);
				_projectRepo.CreateProject(tmp);
				return true;
			}
			else
				return false;
        }

		public bool DeleteProject(int pId)
		{
			var tmp = _projectRepo.FetchProjectById(pId);
			if (tmp != null)
			{
				_projectRepo.DeleteProject(tmp);
				return true;
			}
			else
				return false;
		}

		public ModelProject SeeProjectAsLeader(int lId)
        {
			var r = _projectRepo.GetProjectAsLeader(lId);
			if (r != null)
				return new ModelProject(r.Id, r.Name, _teamRepo.FetchTeamById(r.TeamId).Name, r.Description, r.Regulations, r.Status);
			else
				return null;
		}

		public ModelProject SeeProjectAsCustomer(int cId)
		{
			var r = _projectRepo.GetProjectAsCustomer(cId);
			if (r != null)
				return new ModelProject(r.Id, r.Name, _teamRepo.FetchTeamById(r.TeamId).Name, r.Description, r.Regulations, r.Status);
			else
				return null;
		}

		public List<ModelProject> SeeProjectsAsWorker(int wId)
		{
			var projects = _projectRepo.GetProjectsAsWorker(wId);
			if (projects.FirstOrDefault() != null)
			{
				List<ModelProject> list = new List<ModelProject>();
				foreach (var r in projects)
					list.Add(new ModelProject(r.Id, r.Name, _teamRepo.FetchTeamById(r.TeamId).Name, r.Description, r.Regulations, r.Status));
				return list;
			}
			else
				return null;
		}

		public ModelProject GetProject(int pId)
		{
			var r = _projectRepo.FetchProjectById(pId);
			if (r != null)
			{
				var mp = new ModelProject(r.Id, r.Name, _teamRepo.FetchTeamById(r.TeamId).Name, r.Description, r.Regulations, r.Status);
				return mp;
			}
			else
				return null;
		}

		public bool EditProject(ModelProject mp)
		{
				var p = new Project();
				mp.CopyProject(p, _teamRepo.FetchTeamByName(mp.TeamBuilder).Id);
				_projectRepo.EditProject(p);
				return true;
		}
	}
}

