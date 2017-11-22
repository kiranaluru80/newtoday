using System;
using SQLite;

namespace SampleDB
{
    public class Employee
    {
        [PrimaryKey,AutoIncrement]
        public int EmpId
        {
            get;
            set;
            
        }
        public string EmpName{

            get;
            set;
        }

        public string Designation{
            get;

            set;

        }
        public int Age
        {
            get;
            set;
        }
    }

	


}
