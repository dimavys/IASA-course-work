using System;
using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class RepositoryService : IRepositoryService
	{
		private readonly IRepositoryRepo _repositoryRepo;
		private readonly IProjectRepo _projectRepo;
		private readonly IUserRepo _userRepo;

		public RepositoryService(IProjectRepo projectRepo, IRepositoryRepo repositoryRepo, IUserRepo userRepo)
		{ 
			_projectRepo = projectRepo;
			_repositoryRepo = repositoryRepo;
			_userRepo = userRepo;
		}

		public List<ModelRepository> SeeRepositoriesAsLeader(int lid)
        {
			var tmp = _repositoryRepo.GetRepositoriesAsLeader(lid);
			var list = new List<ModelRepository>();
			foreach (var r in tmp)
				list.Add(new ModelRepository(r.Id, r.Name));
			return list;

        }

		public List<ModelRepository> SeeRepositoriesAsWorker(int pid)
        {
			var tmp = _repositoryRepo.GetRepositoriesAsWorker(pid);
			if (tmp.FirstOrDefault() != null)
			{
				var list = new List<ModelRepository>();
				foreach (var r in tmp)
					list.Add(new ModelRepository(r.Id, r.Name));
				return list;
			}
			return null;
		}

		public bool DeleteRepository(int id)
        {
			var tmp = _repositoryRepo.FetchRepositoryById(id);
			if (tmp != null)
			{
				_repositoryRepo.DeleteRepository(tmp);
				return true;
			}
			return false;
        }

		public bool CreateRepository(ModelRepository mr,int leadId)
        {
			if (_userRepo.FetchUserById(leadId) != null)
            {
				if (_repositoryRepo.FetchRepositoryByName(mr.NameBuilder) != null)
					return false;
				else
                {
					var tmp = new Repository();
					var project = _projectRepo.FetchProjectByLeader(leadId);
					if (project != null)
                    {
						mr.CopyRepository(tmp, project.Id);
						_repositoryRepo.CreateRepository(tmp);
						return true;
					}
                }
            }
			return false;
        }

		public ModelRepository GetRepository(int id)
		{
			var tmp = _repositoryRepo.FetchRepositoryById(id);
			if (tmp != null)
			{
				var mr = new ModelRepository(tmp.Id, tmp.Name);
				return mr;
			}
			return null;
		}

		public bool EditRepository (ModelRepository mr)
        {
			var tmp = _repositoryRepo.FetchRepositoryById(mr.IdBuilder);
			if (tmp != null)
            {
				var r = new Repository();
				mr.CopyRepository(r,tmp.ProjectId);
				_repositoryRepo.EditRepository(r);
				return true;
            }
			return false;
        }
	}
}

