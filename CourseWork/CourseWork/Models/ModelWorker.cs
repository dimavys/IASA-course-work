using CourseWork.Data;

namespace CourseWork.Models
{
    public class ModelWorker : ModelUser
    { 
        private double salary;

        public ModelWorker() : base()
        {
            salary = 0;
        }

        public ModelWorker(int _id, string _name, string _surname, string _login, string _password, string _role, double _salary)
            : base(_id, _name, _surname, _login, _password, _role)
        {
            salary = _salary;
        }

        public double SalaryBuilder { get { return salary; } set { salary = value; } }

        public override void CopyData(User u, int _roleId)
        {
            base.CopyData(u, _roleId);
            u.Salary = salary;
        }
    }
}

