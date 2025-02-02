using SQLite;
using System;
using System.Collections.Generic;
using CosplayApp.Resources;

namespace CosplayApp
{
    public class ToDoItemDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<ToDoItem>();
        }

        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<ToDoItem>().ToListAsync();
        }

        public async Task<List<ToDoItem>> GetItemsByCardAsync(CosplayCard cosplaycard)
        {
            await Init();
            return await Database.Table<ToDoItem>().Where(i => cosplaycard.ID == i.CosplayCardId).ToListAsync();
        }

        public async Task<ToDoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<ToDoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(ToDoItem item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(ToDoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
