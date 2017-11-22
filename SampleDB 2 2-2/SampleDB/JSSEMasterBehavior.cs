using System;
using SQLite;

namespace SampleDB
{
    public class JSSEMasterBehavior
    {
		[PrimaryKey]
		public int Behavior_ID
		{
			get;
			set;

		}
		public string Behavior
		{

			get;
			set;
		}
        public int Category_ID
        {
			get;
			set;
        }
		public int Org_ID
		{
			get;

			set;

		}
		public int BehaviorType_ID
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
