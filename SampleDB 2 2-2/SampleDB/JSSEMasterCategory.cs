using System;
using SQLite;

namespace SampleDB
{
    public class JSSEMasterCategory
    {
		[PrimaryKey]
		public int Category_ID
		{
			get;
			set;

		}
		public string Category
		{

			get;
			set;
		}

		public DateTime CreatedDate
		{
			get;
			set;
		}
		public string CreatedBy
		{
			get;
			set;
		}
		public DateTime ModifiedDate
		{
			get;
			set;
		}
		public string ModifiedBy
		{
			get;
			set;
		}
		public bool Active
		{
			get;
			set;
		}
    }

}
