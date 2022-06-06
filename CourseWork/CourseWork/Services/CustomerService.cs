using System;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly IUserRepo _userRepo;
		private readonly IRoleRepo _roleRepo;

		public CustomerService(IUserRepo userRepo, IRoleRepo roleRepo)
		{
			_userRepo = userRepo;
			_roleRepo = roleRepo;
		}

		public List<ModelUser> SeeCustomers()
        {
			var users = _userRepo.GetCustomers();
			if (users.FirstOrDefault() != null)
            {
				var list = new List<ModelUser>();
				foreach (var r in users)
					list.Add(new ModelUser(r.Id, r.Name, r.Surname, r.Login, r.Password, _roleRepo.FetchRoleById(r.RoleId).Name));
				return list;
            }
			return null;
        }
	}
}

