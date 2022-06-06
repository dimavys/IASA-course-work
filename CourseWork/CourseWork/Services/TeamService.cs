using System;
using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;
using CourseWork.Repos;

namespace CourseWork.Services
{
    //controller (view data) => service (business logic) => repo (db)
    public class TeamService : ITeamService
    {
        private readonly ITeamRepo _teamRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;

        public TeamService(ITeamRepo teamRepo, IUserRepo userRepo, IRoleRepo roleRepo)
        {
            _teamRepo = teamRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public bool AddWorker(int teamId, ModelWorker worker)
        {
            var user = _userRepo.FetchUserByLogin(worker.LoginBuilder);
            if (user != null)
            {
                if (_teamRepo.FecthWorkingByWorkerLogin(worker.LoginBuilder) != null)
                    return false;
                else
                {
                    _teamRepo.AddWorker(teamId, user.Id);
                    return true;
                }
            }
             return false;
        }

        public bool RemoveWorker(int teamId, int workerId)
        {
            var user = _userRepo.FetchUserById(workerId);
            if (user != null)
            {
                _teamRepo.RemoveWorker(teamId, workerId);
                return true;
            }
            return false;
        }

        public bool CreateTeam(ModelTeam team)
        {
            var tmp = _teamRepo.FetchTeamByName(team.NameBuilder);
            var a = _teamRepo.FetchTeamByCustomer(team.CustomerLoginBuilder);
            var b = _teamRepo.FetchTeamByLeader(team.TeamLeadLoginBuilder);
            if (tmp != null || a != null || b != null)
                return false;
            else
            {
                var existingLeader = _userRepo.FetchUserByLogin(team.TeamLeadLoginBuilder);
                var existingCustomer = _userRepo.FetchUserByLogin(team.CustomerLoginBuilder);
                if (existingCustomer != null && existingLeader != null)
                {
                    var teamEntity = new Team();
                    team.CopyTeam(teamEntity, existingCustomer.Id, existingLeader.Id);
                    _teamRepo.CreateTeam(teamEntity);
                    return true;
                }
            return false;
            }
        }

        public List<ModelTeam> SeeTeams()
        {
            var teams = _teamRepo.GetTeams();
            List<ModelTeam> list = new List<ModelTeam>();
            foreach (var t in teams)
            {
                var customer = _userRepo.FetchUserById(t.CustomerId);
                var leader = _userRepo.FetchUserById(t.TeamLeadId);
                list.Add(new ModelTeam(t.Id,t.Name,leader.Login,customer.Login,t.Rating));
            }
            return list;
        }

        public List<ModelTeam> SeeTeamsAsWorker(int userId)
        {
            var teams = _teamRepo.GetTeams(userId);
            List<ModelTeam> list = new List<ModelTeam>();
            foreach (var t in teams)
            {
                var customer = _userRepo.FetchUserById(t.CustomerId);
                var leader = _userRepo.FetchUserById(t.TeamLeadId);
                list.Add(new ModelTeam(t.Id, t.Name, leader.Login, customer.Login, t.Rating));
            }
            return list;
        }

        public bool DeleteTeam(int teamId)
        {
            var tmp = _teamRepo.FetchTeamById(teamId);
            if (tmp != null)
            {
                _teamRepo.DeleteTeam(tmp);
                return true;
            }
            return false;
        }

        public List<ModelWorker> SeeWorkers(int teamId)
        {
            var workers = _userRepo.GetWorkersByTeam(teamId);
            List<ModelWorker> list = new List<ModelWorker>();
            foreach (var w in workers)
                list.Add(new ModelWorker(w.Id, w.Name, w.Surname, w.Login, w.Password, _roleRepo.FetchRoleById(w.RoleId).Name, (double)w.Salary));
            return list;
        }
    }
}

