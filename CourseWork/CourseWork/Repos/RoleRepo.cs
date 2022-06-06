using System;
using CourseWork.Data;
using CourseWork.Interfaces;

namespace CourseWork.Repos
{
	public class RoleRepo : IRoleRepo
	{
		private readonly AppDbContext _appDbContext;

		public RoleRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public Role FetchRoleById(int roleId)
		{
			var tmp = _appDbContext.Roles.Where(x => x.Id == roleId).FirstOrDefault();
			return tmp;
		}

		public Role FetchRoleByName(string name)
        {
			var tmp = _appDbContext.Roles.Where(x => x.Name == name).FirstOrDefault();
			return tmp;
        }

		public string GetRoleName(int userKey)
        {
			var tmp = (from r in _appDbContext.Roles
					   join u in _appDbContext.Users
					   on r.Id equals u.RoleId
					   where u.Id == userKey
					   select r).FirstOrDefault();
			return tmp.Name;
		}
	}
}
