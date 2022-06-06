using System;
using CourseWork.Data;
namespace CourseWork.Models
{
	public class ModelRepository
	{
		private int id;

		private string name;

		public ModelRepository()
		{
			name = "default";
		}

		public ModelRepository(int _id, string _name)
		{
			id = _id;
			name = _name;
		}

		public int IdBuilder { get { return id; } set { id = value; } }

		public string NameBuilder { get { return name; } set { name = value; } }

		public void CopyRepository(Repository r, int pId)
		{
			r.Id = id;
			r.Name = name;
			r.ProjectId = pId;
		}
	}
}

