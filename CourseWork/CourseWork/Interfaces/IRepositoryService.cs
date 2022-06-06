using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IRepositoryService
	{
		List<ModelRepository> SeeRepositoriesAsLeader(int lid);
		List<ModelRepository> SeeRepositoriesAsWorker(int rid);
		bool DeleteRepository(int id);
		bool CreateRepository(ModelRepository mr, int leadId);
		bool EditRepository(ModelRepository mr);
		ModelRepository GetRepository(int id);
	}
}

