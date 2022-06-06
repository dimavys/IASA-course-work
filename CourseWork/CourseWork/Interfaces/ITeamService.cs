using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
        public interface ITeamService
        {
            bool AddWorker(int teamId, ModelWorker worker);
            bool RemoveWorker(int teamId, int workerId);
            bool CreateTeam(ModelTeam team);
            List<ModelTeam> SeeTeams();
            List<ModelTeam> SeeTeamsAsWorker(int userId);
            bool DeleteTeam(int teamId);
            List<ModelWorker> SeeWorkers(int teamId);
    }   
}

