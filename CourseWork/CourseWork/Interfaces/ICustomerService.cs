using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface ICustomerService
	{
		List<ModelUser> SeeCustomers();
	}
}

