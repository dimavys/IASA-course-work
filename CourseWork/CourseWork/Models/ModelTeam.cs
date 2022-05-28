using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelTeam
	{
		private int id;

		public int IdBuilder { get { return id; } set { id = value; } }

		private string name;

		public string NameBuilder { get { return name; } set { name = value; } }

		private string teamLeadLogin;

		public string TeamLeadLoginBuilder { get { return teamLeadLogin; } set { teamLeadLogin = value; } }

		private string customerLogin;

		public string CustomerLoginBuilder { get { return customerLogin; } set { customerLogin = value; } }

		private int rating;
	
		public int RatingBuilder { get { return rating; } set {rating = value;}}

		public void CopyTeam(Team t, int _customerId, int _teamLeadId)
        {
			t.Name = name;
			t.Rating = rating;
			t.TeamLeadId = _teamLeadId;
			t.CustomerId = _customerId;
        }

	}
}

