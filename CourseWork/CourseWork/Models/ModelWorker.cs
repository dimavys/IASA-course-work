using System;
using CourseWork.Data;

namespace CourseWork.Models
{
    public class ModelWorker : ModelUser
    {
        private double salary;

        public double SalaryBuilder { get { return salary; } set { salary = value; } }

        public override void CopyData(User u, int _roleId)
        {
            base.CopyData(u, _roleId);
            u.Salary = salary;
        }

        //public override void CopyData(User u, string _role, double _salary)
        //{
        //    base.CopyData(u, _role, _salary);
        //    salary = _salary;
        //}
    }
}

