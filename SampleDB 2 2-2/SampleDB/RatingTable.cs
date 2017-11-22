
using System; using SQLite; using SQLite.Extensions; using System.ComponentModel;
 namespace SampleDB
{
    public class RatingTable
    {
		[PrimaryKey]
		public int Rating_ID
		{
			get;
			set;
		}
		public string Rating
		{
			get;
			set;
		}
		public bool RatingChecked
		{
			get;
			set;
		}
		public bool Selected
		{
			get;
			set;
		}
	}
}
