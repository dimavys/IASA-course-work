using System;
using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Repos
{
	public class TeamRepo : BaseRepo<Team> , ITeamRepo
	{
        public TeamRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public void CreateTeam(Team team)
        {
            Create(team);
        }

        public void DeleteTeam(Team team)
        {
            Delete(team);
        }

        public void UpdateRating(int id, int rating)
        {
            var existingTeam = _appDbContext.Teams.Where(x => x.Id == id).First();

            existingTeam.Rating = rating;

            _appDbContext.SaveChanges();
        }

        public void AddWorker(int idTeam, int idWorker)
        {
            _appDbContext.Workings.Add(new Working() { TeamId = idTeam, WorkerId = idWorker });
            _appDbContext.SaveChanges();
        }

        public Working FecthWorkingByWorkerLogin(string login)
        {
            var tmp = (from u in _appDbContext.Users
                       join wk in _appDbContext.Workings
                       on u.Id equals wk.WorkerId
                       where u.Login == login
                       select wk).FirstOrDefault();
            return tmp;
        }

        public void RemoveWorker(int idTeam, int idWorker)
        {
            _appDbContext.Workings.Remove(new Working() { TeamId = idTeam, WorkerId = idWorker });
            _appDbContext.SaveChanges();
        }

        public Team FetchTeamById(int teamId)
        {
            var team = _appDbContext.Teams.
                Where(x => x.Id == teamId)
                .FirstOrDefault();
            return team;
        }

        public Team FetchTeamByName(string name)
        {
            var team = _appDbContext.Teams.
                Where(x => x.Name == name)
                .FirstOrDefault();
            return team;
        }

        public Team FetchTeamByLeader(string login)
        {
            var team = (from t in _appDbContext.Teams
                         join l in _appDbContext.Users
                         on t.TeamLeadId equals l.Id
                         where l.Login == login
                         select t).FirstOrDefault();
            return team;
        }

        public Team FetchTeamByCustomer(string login)
        {
            var team = (from t in _appDbContext.Teams
                        join c in _appDbContext.Users
                        on t.CustomerId equals c.Id
                        where c.Login == login
                        select t).FirstOrDefault();
            return team;
        }

        public List<Team> GetTeams()
        {
            var tmp = (from u in _appDbContext.Users
                       join t in _appDbContext.Teams
                       on u.Id equals t.CustomerId
                       join u1 in _appDbContext.Users
                       on t.TeamLeadId equals u1.Id
                       select t).ToList();
            return tmp;
        }

        public List<Team> GetTeams(int id)
        {
            var tmp = (from u in _appDbContext.Users
                       join wks in _appDbContext.Workings
                       on u.Id equals wks.WorkerId
                       join t in _appDbContext.Teams
                       on wks.TeamId equals t.Id
                       where u.Id == id
                       select t).ToList();
            return tmp;
        }
    }
}

