using System;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IUserRepo
	{
		User FetchUserByLogin(string login);
		User FetchUserById(int id);
		List<User> GetWorkersByTeam(int teamId);
		List<User> GetWorkers(int adminId);
		List<User> GetWorkersInProject(int teamLeadId);
		List<User> GetCustomers();
		void CreateUser(User w);
		void DeleteWorker(User w);
		void EditUser(User w);
	}
}

