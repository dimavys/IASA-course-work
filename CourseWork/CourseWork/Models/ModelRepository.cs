using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelRepository
	{
		private int id;

		public int IdBuilder { get { return id; } set { id = value; } }

		private string name;

		public string NameBuilder { get { return name; } set { name = value; } }

		public void CopyRepository(Repository r, int pId)
		{
			r.Name = name;
			r.ProjectId = pId;
		}
	}
}

