using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelProject
	{
		private int id;

		private string name;

		private string team;

		private string description;

		private string regulations { get; set; }

		private string status { get; set; }

		public ModelProject()
		{
			name = "default";
			team = "default";
			description = "default";
			regulations = "default";
			status = "default";
		}

		public ModelProject(int _id, string _name, string _team, string _desc, string _reg, string _status)
		{
			id = _id;
			name = _name;
			team = _team;
			description = _desc;
			regulations = _reg;
			status = _status;
		}

		public int IdBuilder { get { return id; } set { id = value; } }

		public string NameBuilder { get { return name; } set { name = value; } }

		public string TeamBuilder { get { return team; } set { team = value; } }

		public string DescBuilder { get { return description; } set { description = value; } }

		public string RegBuilder { get { return regulations; } set { regulations = value; } }

		public string StatusBuilder { get { return status; } set { status = value; } }

		public void CopyProject (Project p, int teamId)
		{
			p.Id = id;
			p.Name = name;
			p.TeamId = teamId;
			p.Description = description;
			p.Regulations = regulations;
			p.Status = status;
		}

		public void CopyProject(Project p)
		{
			p.Id = id;
			p.Name = name;
			p.Description = description;
			p.Regulations = regulations;
			p.Status = status;
		}
	}
}

