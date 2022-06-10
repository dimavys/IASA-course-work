using System;
namespace CourseWork.Models
{
	public class ModelCustomer : ModelUser
	{
        private string team;

        public ModelCustomer(int _id, string _name, string _surname, string _login, string _password, string _role, string _team)
           : base(_id, _name, _surname, _login, _password, _role)
        {
           team = _team;
        }

        public string TeamBuilder { get { return team; } set { team = value; } }
    }
}

