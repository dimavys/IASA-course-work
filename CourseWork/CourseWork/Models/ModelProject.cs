using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelProject
	{
		private int id;

		public int IdBuilder { get { return id; } set { id = value; } }

		private string name;

		public string NameBuilder { get { return name; } set { name = value; } }

		private string team;

		public string TeamBuilder { get { return team; } set { team = value; } }

		private string description;

		public string DescBuilder { get { return description; } set {description = value; } }

		private string regulations { get; set; }

		public string RegBuilder { get { return regulations; } set { regulations = value; } }

		private string status { get; set; }

		public string StatusBuilder { get { return status; } set { status = value; } }

		public void CopyProject (Project p, int teamId)
		{
			p.Name = name;
			p.TeamId = teamId;
			p.Description = description;
			p.Regulations = regulations;
			p.Status = status;
		}

		public void CopyProject(Project p)
		{
			p.Name = name;
			p.Description = description;
			p.Regulations = regulations;
			p.Status = status;
		}
	}
}

