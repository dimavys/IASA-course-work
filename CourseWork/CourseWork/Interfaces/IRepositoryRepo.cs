using System;
using CourseWork.Data;

namespace CourseWork.Interfaces
{
	public interface IRepositoryRepo
	{
		List<Repository> GetRepositoriesAsLeader(int lid);
		List<Repository> GetRepositoriesAsWorker(int rid);
		void DeleteRepository(Repository r);
		void CreateRepository(Repository r);
		void EditRepository(Repository r);
		Repository FetchRepositoryById(int id);
		Repository FetchRepositoryByName(string name);
	}
}

