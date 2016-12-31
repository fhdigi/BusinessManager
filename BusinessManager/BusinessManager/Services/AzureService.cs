using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace BusinessManager.Services
{
    public class AzureService<T>
    {
        public MobileServiceClient Client { get; set; }
        IMobileServiceSyncTable<T> _table;

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            //Create our client
            Client = new MobileServiceClient(Constants.Constants.AppServiceSite);

            //InitialzeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<T>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            _table = Client.GetSyncTable<T>();
        }

        public async Task<IEnumerable<T>> GetItems()
        {
            await Initialize();
            await SyncItems();
            return await _table.ToEnumerableAsync();
        }

        public async Task<bool> SaveItem(T item)
        {
            await Initialize();
            //await SyncItems();
            await _table.InsertAsync(item);
            return true;
        }

        public async Task SyncItems()
        {
            try
            {
                await Client.SyncContext.PushAsync();
                await _table.PullAsync("allItems", _table.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync speakers, that is alright as we have offline capabilities: " + ex);
            }
        }
    }
}
