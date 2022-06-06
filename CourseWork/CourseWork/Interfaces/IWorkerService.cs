using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkerService
	{
		bool CreateWorker(ModelWorker mw);
		List<ModelWorker> SeeWorkers(int adminId);
		List<ModelWorker> SeeWorkersInProject(int teamLeadId);
		bool DeleteWorker(int wId);
		ModelWorker EditWorker(int wId);
		bool EditWorker(ModelWorker mw);
	}
}

