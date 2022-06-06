using System;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface ITeamRepo
	{
        void CreateTeam(Team team);

        void DeleteTeam(Team team);

        void UpdateRating(int id, int rating);

        void AddWorker(int idTeam, int idWorker);

        void RemoveWorker(int idTeam, int idWorker);

        Team FetchTeamById(int teamId);

        Team FetchTeamByName(string name);

        Team FetchTeamByLeader(string login);

        Team FetchTeamByCustomer(string login);

        List<Team> GetTeams();

        List<Team> GetTeams(int id);

        Working FecthWorkingByWorkerLogin(string login);
    }
}

