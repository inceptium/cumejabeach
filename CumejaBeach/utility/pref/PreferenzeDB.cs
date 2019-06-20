using System;
using SQLite;
using CumejaBeach.utility.pref.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CumejaBeach.utility.pref
{
    public class PreferenzeDB
    {
        readonly SQLiteAsyncConnection database;

        public PreferenzeDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ItemPref>().Wait();
        }

        public Task<List<ItemPref>> GetItemsAsync()
        {
            return database.Table<ItemPref>().ToListAsync();
        }

        public Task<List<ItemPref>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ItemPref>("SELECT * FROM [ItemPref] WHERE [Done] = 0");
        }

        public Task<ItemPref> GetItemAsync(int id)
        {
            return database.Table<ItemPref>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<ItemPref> GetItemByKeyAsync(string key)
        {
            return database.Table<ItemPref>().Where(i => i.key==key).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ItemPref item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ItemPref item)
        {
            return database.DeleteAsync(item);
        }
    }
}
