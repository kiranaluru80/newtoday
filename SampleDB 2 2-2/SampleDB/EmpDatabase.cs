using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
namespace SampleDB
{
    public class EmpDatabase
    {
        readonly SQLiteAsyncConnection database;
        public EmpDatabase(string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<Employee>().Wait();
            database.CreateTableAsync<JSSEMasterBehavior>().Wait();
            database.CreateTableAsync<JSSEMasterCategory>().Wait();
            database.CreateTableAsync<RatingTable>().Wait();

		}
		public Task<List<Employee>> GetEmployeeAsync()
		{
			return database.Table<Employee>().ToListAsync();
		}
		public Task<Employee> GetEmployeeAsync(int id)
		{
			return database.Table<Employee>().Where(i => i.EmpId == id).FirstOrDefaultAsync();
		}

		public Task<int> SaveEmployeeAsync(Employee employee)
		{
			//if (employee.EmpId != 0)
			//{
			//	return database.UpdateAsync(employee);

			//}
			//else
			//{
				return database.InsertAsync(employee);
			//}
		}
		public Task<int> DelteEmployeeAsync(Employee employee)
		{
			return database.DeleteAsync(employee);
		}
  //      public Task<List<JSSEMasterBehavior>> GetJsseBehaviors()
		//{
  //          return database.Table<JSSEMasterBehavior>().ToListAsync();
		//}

        public Task<List<JSSEMasterBehavior>> GetJsseBehaviors(int orgId, int CategoryId)
		{
            return database.Table<JSSEMasterBehavior>().Where(i => i.Org_ID == orgId && i.Category_ID == CategoryId).ToListAsync();
		}

        public Task<int> SaveBehaviorssAsync(JSSEMasterBehavior behavior)
		{
			//if (employee.EmpId != 0)
			//{
			//  return database.UpdateAsync(employee);

			//}
			//else
			//{
			return database.InsertAsync(behavior);
			//}
		}
		public Task<int> DelteBehaviorsAsync(JSSEMasterBehavior employee)
		{
			return database.DeleteAsync(employee);
		}
        public Task<List<JSSEMasterCategory>> GetJsseCategories()
		{
            return database.Table<JSSEMasterCategory>().ToListAsync();
		}
		public Task<JSSEMasterCategory> GGetJsseCategorieset(int id)
		{
            return database.Table<JSSEMasterCategory>().Where(i => i.Category_ID == id).FirstOrDefaultAsync();
		}

        public Task<int> SaveCategoriesAsync(JSSEMasterCategory employee)
		{
			//if (employee.EmpId != 0)
			//{
			//  return database.UpdateAsync(employee);

			//}
			//else
			//{
			return database.InsertAsync(employee);
			//}
		}
        public Task<int> DelteEmployeeAsync(JSSEMasterCategory employee)
		{
			return database.DeleteAsync(employee);
		}
        public Task<List<RatingTable>> GetRatings()
		{
            return database.Table<RatingTable>().ToListAsync();
		}
		public Task<RatingTable> GetRatingsset(int id)
		{
            return database.Table<RatingTable>().Where(i => i.Rating_ID == id).FirstOrDefaultAsync();
		}

        public Task<int> SaveRatingsAsync(RatingTable rating)
		{
			//if (employee.EmpId != 0)
			//{
			//  return database.UpdateAsync(employee);

			//}
			//else
			//{
			return database.InsertAsync(rating);
			//}
		}
        public Task<int> DelteRatingAsync(RatingTable rating)
		{
			return database.DeleteAsync(rating);
		}
    }
}
