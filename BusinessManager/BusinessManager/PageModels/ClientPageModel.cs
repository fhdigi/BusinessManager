using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class ClientPageModel
    {
        public bool IsBusy { get; set; }
        public Client CurrentClient { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        #region Commands

        private Command _saveClientCommand;

        public Command SaveClientCommand
        {
            get
            {
                return _saveClientCommand ??
                       (_saveClientCommand = new Command(async () => await ExecuteSaveClientCommand()));
            }
        }

        Command _refreshCommand;

        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteRefreshCommand()));
            }
        }

        #endregion

        public ClientPageModel()
        {
            //var client = EasyMobileServiceClient.Current;
            //Clients = new ObservableCollection<Client>(client.Table<Client>());

            CurrentClient = new Client();

            ExecuteRefreshCommand();
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //var service = DependencyService.Get<AzureService>();
                //await Clients.Refresh();
            }
            catch (Exception ex)
            {
                //await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteSaveClientCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (CurrentClient != null)
                {
                    // Save the supplier 
                    //var client = EasyMobileServiceClient.Current;
                    //await client.Table<Client>().AddAsync(CurrentClient);

                    // close the screen 
                    // await CoreMethods.PopPageModel();
                }
            }
            catch (Exception ex)
            {
                // await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
