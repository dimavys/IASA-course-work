using System;
using CourseWork.Data;

namespace CourseWork.Models
{
    public class ModelUser
    {
        protected int id;

        public int IdBuilder { get { return id; } set { id = value; } }

        protected string name;

        public string NameBuilder { get { return name; } set { name = value; } }

        protected string surname;

        public string SurnameBuilder { get { return surname; } set { surname = value; } }

        protected string login;

        public string LoginBuilder { get { return login; } set { login = value; } }

        protected string password;

        public string PasswordBuilder { get { return password; } set { password = value; } }

        protected string role;

        public string RoleBuilder { get { return role; } set { role = value; } }

        //protected int roleId;

        //public int RoleIdBuilder { get { return roleId; } set { roleId = value; } }

        public virtual void CopyData(User u, int _roleId)
        {
            u.Login = login;
            u.Password = password;
            u.Name = name;
            u.Surname = surname;
            u.RoleId = _roleId;
        }

        //public virtual void CopyData(User u, string _role, double _salary)
        //{
        //    id = u.Id;
        //    login = u.Login;
        //    password = u.Password;
        //    name = u.Name;
        //    surname = u.Surname;
        //    role = _role;
        //}
    }
}
