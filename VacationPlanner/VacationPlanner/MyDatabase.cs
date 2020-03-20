using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VacationPlanner
{
    public class MyDatabase
    {
        readonly SQLiteAsyncConnection database;
        public MyDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Trip>().Wait();
            database.CreateTableAsync<Event>().Wait();
            database.CreateTableAsync<ShoppingBagModel>().Wait();
        }
        public Task<List<Trip>> GetTripItemsAsync()
        {
            return database.Table<Trip>().ToListAsync();
        }
        public Task<Trip> GetTripItemsAsync(int id)
        {
            return database.Table<Trip>().Where(i => i.TripID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveTripItemAsync(Trip item)
        {
            if (item.TripID != 0)
            {
                //return database.InsertAsync(item);
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteTripItemAsync(Trip item)
        {
            return database.DeleteAsync(item);
        }
        public Task<List<Event>> GetEventItemsAsync()
        {
            return database.Table<Event>().ToListAsync();
        }
        public Task<Event> GetEventItemsAsync(int id)
        {
            return database.Table<Event>().Where(i => i.EventID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveEventItemAsync(Event item)
        {
            if (item.EventID != 0)
            {
                //return database.InsertAsync(item);
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteEventItemAsync(Event itm)
        {
            return database.DeleteAsync(itm);
        }
        public Task<List<ShoppingBagModel>> GetBagItemsAsync()
        {
            return database.Table<ShoppingBagModel>().ToListAsync();
        }
        public Task<ShoppingBagModel> GetBagItemsAsync(int id)
        {
            return database.Table<ShoppingBagModel>().Where(i => i.BagID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveBagItemsAsync(ShoppingBagModel item)
        {

            if (item.BagID != 0)
            {
                //return database.InsertAsync(item);
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
       
        }
        public Task<int> DeleteBagItemsAsync(ShoppingBagModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}