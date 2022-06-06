using System;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepo _userRepo;
		private readonly IRoleRepo _roleRepo;

		public AuthService(IUserRepo userRepo, IRoleRepo roleRepo)
		{
			_userRepo = userRepo;
			_roleRepo = roleRepo;
		}

		public bool SignUp(ModelUser mu)
		{
			var user = _userRepo.FetchUserByLogin(mu.LoginBuilder);
			if (user != null)
				return false;
			else
            {
				mu.RoleBuilder = "Customer";
				user = new Data.User();
				mu.CopyData(user, _roleRepo.FetchRoleByName(mu.RoleBuilder).Id);
				_userRepo.CreateUser(user);
				return true;
            }
		}

		public bool LogIn(ModelUser mu)
        {
			var user = _userRepo.FetchUserByLogin(mu.LoginBuilder);
			if (user != null)
            {
				if (user.Password == mu.PasswordBuilder)
					return true;
            }
			return false;
		}

		public int GetId(ModelUser mu)
        {
			var user = _userRepo.FetchUserByLogin(mu.LoginBuilder);
			return user.Id;
		}

		public string GetRoleName(int userKey)
        {
			return _roleRepo.GetRoleName(userKey);
		}

		public ModelUser GetUser(int id)
		{
			var user = _userRepo.FetchUserById(id);
			if (user != null)
            {
				var mu = new ModelUser();
				mu.NameBuilder = user.Name;
				return mu;
			}
			return null;
		}
	}
}

