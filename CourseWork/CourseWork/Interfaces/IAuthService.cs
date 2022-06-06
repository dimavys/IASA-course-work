using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IAuthService
	{
		bool SignUp(ModelUser mU);
		bool LogIn(ModelUser mU);
		int GetId(ModelUser mu);
		string GetRoleName(int userKey);
		ModelUser GetUser(int id);
	}
}

