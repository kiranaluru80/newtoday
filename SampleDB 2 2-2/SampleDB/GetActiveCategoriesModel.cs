using System;
using System.Collections.Generic;

namespace SampleDB
{
    public class GetActiveCategoriesModel
    {
		public class EntBehavior
		{
			public int Behavior_ID { get; set; }
			public string Behavior { get; set; }
			public string BehaviorDesc { get; set; }
			public int BehaviorType_ID { get; set; }
			public string BehaviorType { get; set; }
			public object Org_ID { get; set; }
			public bool BehviorChecked { get; set; }
			public bool IsActive { get; set; }
			public object Category_ID { get; set; }
		}
		public class OrgBehaviors
		{
			public int Behavior_ID { get; set; }
			public string Behavior { get; set; }
			public string BehaviorDesc { get; set; }
			public int BehaviorType_ID { get; set; }
			public string BehaviorType { get; set; }
			public int Org_ID { get; set; }
			public bool BehviorChecked { get; set; }
			public bool IsActive { get; set; }
			public object Category_ID { get; set; }
		}

		public class Ratings
		{
			public int Rating_ID { get; set; }
			public string Rating { get; set; }
			public bool RatingChecked { get; set; }
			public bool Selected { get; set; }
		}

		public class RootObject
		{
			public int Category_ID { get; set; }
			public string Category { get; set; }
			public string CategoryDesc { get; set; }
			public List<EntBehavior> EntBehaviors { get; set; }
			//public object OrgBehaviors { get; set; }
			public List<List<OrgBehaviors>> AllOrgBehaviors { get; set; }
			public List<Ratings> Ratings { get; set; }
			public object Comments { get; set; }
			public object RatingID { get; set; }
			public bool IsActive { get; set; }
			public object JSSE_ID { get; set; }
		}
		
	}
}
