using System;
using CourseWork.Data;

namespace CourseWork.Interfaces
{
	public interface IRoleRepo
	{
		Role FetchRoleById(int roleId);
		Role FetchRoleByName(string name);
		string GetRoleName(int userKey);
	}
}

