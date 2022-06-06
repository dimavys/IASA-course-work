using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class WorkerService : IWorkerService
	{
        private readonly ITeamRepo _teamRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;

        public WorkerService(ITeamRepo teamRepo, IUserRepo userRepo, IRoleRepo roleRepo)
        {
            _teamRepo = teamRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public bool CreateWorker(ModelWorker mw)
        {
            var r = _roleRepo.FetchRoleByName(mw.RoleBuilder);
            if ( r != null)
            {
                if (_userRepo.FetchUserByLogin(mw.LoginBuilder) != null)
                    return false;
                else
                {
                    var w = new User();
                    mw.CopyData(w, r.Id);
                    _userRepo.CreateUser(w);
                    return true;
                }
            }
            else
                return false;
        }

        public List<ModelWorker> SeeWorkers(int adminId)
        {
            var workers = _userRepo.GetWorkers(adminId);
            List<ModelWorker> list = new List<ModelWorker>();
            foreach (var w in workers)
                list.Add(new ModelWorker(w.Id, w.Name, w.Surname, w.Login, w.Password, _roleRepo.FetchRoleById(w.RoleId).Name, (double)w.Salary));
            return list;
        }

        public List<ModelWorker> SeeWorkersInProject(int teamLeadId)
        {
            var workers = _userRepo.GetWorkersInProject(teamLeadId);
            List<ModelWorker> list = new List<ModelWorker>();
            foreach (var w in workers)
                list.Add(new ModelWorker(w.Id, w.Name, w.Surname, w.Login, w.Password, _roleRepo.FetchRoleById(w.RoleId).Name, (double)w.Salary));
            return list;
        }

        public bool DeleteWorker(int wId)
        {
            var tmp = _userRepo.FetchUserById(wId);
            if (tmp != null)
            {
                _userRepo.DeleteWorker(tmp);
                return true;
            }
            return false;
        }

        public ModelWorker EditWorker(int wId)
        {
           var w = _userRepo.FetchUserById(wId);
           var wk = new ModelWorker(w.Id, w.Name, w.Surname, w.Login, w.Password, _roleRepo.FetchRoleById(w.RoleId).Name, (double)w.Salary);
           return wk;
        }

        public bool EditWorker(ModelWorker mw)
        {
            if (_userRepo.FetchUserById(mw.IdBuilder) != null)
            {
                if (_userRepo.FetchUserByLogin(mw.LoginBuilder) != null)
                    return false;
                var role = _roleRepo.FetchRoleByName(mw.RoleBuilder);
                var w = new User();
                mw.CopyData(w, role.Id);
                _userRepo.EditUser(w);
                return true;
            }
            return false;
        }
    }
}

