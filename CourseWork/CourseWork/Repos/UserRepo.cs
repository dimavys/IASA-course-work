using System;
using CourseWork.Controllers;
using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Repos
{
    public class UserRepo : BaseRepo<User> ,IUserRepo
    {
        public UserRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public User FetchUserByLogin(string login)
        {
            var user = _appDbContext.Users
                .Where(x => x.Login == login)
                .FirstOrDefault();
            return user;
        }

        public User FetchUserById(int id)
        {
            var user = _appDbContext.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return user;
        }

        public List<User> GetWorkersByTeam(int teamId)
        {
            var tmp = (from r in _appDbContext.Roles
                       join u in _appDbContext.Users
                       on r.Id equals u.RoleId
                       join w in _appDbContext.Workings
                       on u.Id equals w.WorkerId
                       where w.TeamId == teamId
                       select u).ToList();
            return tmp;
        }

        public List<User> GetWorkers(int adminId)
        {
            var tmp = (from r in _appDbContext.Roles
                       join u in _appDbContext.Users
                       on r.Id equals u.RoleId
                       where r.Name != "Customer"
                       && u.Id != adminId
            select u).ToList();
            return tmp;
        }

        public List<User> GetCustomers()
        {
            var tmp = (from r in _appDbContext.Roles
                       join u in _appDbContext.Users
                       on r.Id equals u.RoleId
                       join t in _appDbContext.Teams
                       on u.Id equals t.CustomerId into Team
                       from t in Team.DefaultIfEmpty()
                       where r.Name == "Customer" && t == null
                       select u).ToList();
            return tmp;
        }

        public List<User> GetWorkersInProject(int teamLeadId)
        {
            var tmp = (from r in _appDbContext.Roles
                       join u in _appDbContext.Users
                       on r.Id equals u.RoleId
                       join t in _appDbContext.Teams
                       on u.Id equals t.TeamLeadId
                       join ws in _appDbContext.Workings
                       on t.Id equals ws.TeamId
                       join w in _appDbContext.Users
                       on ws.WorkerId equals w.Id
                       where u.Id == teamLeadId
                       select w).ToList();
            return tmp;
        }

        public void CreateUser(User u)
        {
           Create(u);
        }

        public void DeleteWorker(User worker)
        {
            _appDbContext.Users.Remove(worker);
            _appDbContext.SaveChanges();
        }

        public void EditUser(User w)
        {
            var tmp = FetchUserById(w.Id);
            tmp.Name = w.Name;
            tmp.Surname = w.Surname;
            tmp.Login = w.Login;
            tmp.Password = w.Password;
            tmp.RoleId = w.RoleId;
            tmp.Salary = w.Salary;
            _appDbContext.SaveChanges();
        }
    }
}

