using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelTeam
	{
		private int id;

		private string name;

		private string teamLeadLogin;

		private string customerLogin;

		private int rating;

		public ModelTeam()
		{
			rating = 0;
		}

		public ModelTeam(int _id, string _name, string _teamLeadLogin, string _customerLogin, int _rating)
		{
			id = _id;
			name = _name;
			teamLeadLogin = _teamLeadLogin;
			customerLogin = _customerLogin;
			rating = _rating;
		}

		public int IdBuilder { get { return id; } set { id = value; } }

		public string NameBuilder { get { return name; } set { name = value; } }

		public string TeamLeadLoginBuilder { get { return teamLeadLogin; } set { teamLeadLogin = value; } }

		public string CustomerLoginBuilder { get { return customerLogin; } set { customerLogin = value; } }

		public int RatingBuilder { get { return rating; } set { rating = value; } }

		public void CopyTeam(Team t, int _customerId, int _teamLeadId)
        {
			t.Id = id;
			t.Name = name;
			t.Rating = rating;
			t.TeamLeadId = _teamLeadId;
			t.CustomerId = _customerId;
        }

	}
}

