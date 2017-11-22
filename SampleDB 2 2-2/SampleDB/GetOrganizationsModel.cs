using System;
using System.Collections.Generic;

namespace SampleDB
{
    public class GetOrganizationsModel
    {
		public class Section
		{
			public string Section_Id { get; set; }
			public string Section_Name { get; set; }
		}

		public class Department
		{
			public string Dept_Id { get; set; }
			public string Dept_Name { get; set; }
			public List<Section> Sections { get; set; }
		}

		public class Organization
		{
			public string Org_Id { get; set; }
			public string Org_Name { get; set; }
			public List<Department> Departments { get; set; }
			public object MajorGroup_Id { get; set; }
			public object MajorGroup_Name { get; set; }
		}

		public class RootObject
		{
			public string MajorGroup_Id { get; set; }
			public string MajorGroup_Name { get; set; }
			public List<Organization> Organizations { get; set; }
		}
    }
}

