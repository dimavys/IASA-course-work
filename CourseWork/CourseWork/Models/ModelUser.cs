using CourseWork.Data;

namespace CourseWork.Models
{
    public class ModelUser
    {
        protected int id;

        protected string name;

        protected string surname;

        protected string login;     

        protected string password;

        protected string role;

        public ModelUser()
        {
            name = null;
            surname = null;
            login = null;
            password = null;
            role = null;
        }

        public ModelUser(int _id, string _name, string _surname, string _login, string _password, string _role)
        {
            id = _id;
            name = _name;
            surname = _surname;
            login = _login;
            password = _password;
            role = _role;
        }

        public int IdBuilder { get { return id; } set { id = value; } }

        public string NameBuilder { get { return name; } set { name = value; } }

        public string SurnameBuilder { get { return surname; } set { surname = value; } }

        public string LoginBuilder { get { return login; } set { login = value; } }

        public string PasswordBuilder { get { return password; } set { password = value; } }

        public string RoleBuilder { get { return role; } set { role = value; } }

        public virtual void CopyData(User u, int _roleId)
        {
            u.Id = id;
            u.Login = login;
            u.Password = password;
            u.Name = name;
            u.Surname = surname;
            u.RoleId = _roleId;
        }
    }
}
