using CosplayApp.Resources;
using SQLite;

namespace CosplayApp;

    public class CosPlanItemDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<CosplayCard>();
        }

        public async Task<List<CosplayCard>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<CosplayCard>().ToListAsync();
        }

        public async Task<CosplayCard> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<CosplayCard>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(CosplayCard item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(CosplayCard item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
