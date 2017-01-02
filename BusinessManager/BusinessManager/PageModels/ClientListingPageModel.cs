using System;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using Xamarin.Forms;
using System.Linq;

namespace BusinessManager.PageModels
{
    public class ClientListingPageModel : BasePageModel
    {
        #region Properties

        public Client SelectedClient { get; set; }
        public ObservableRangeCollection<Client> Clients { get; set; }

        #endregion

        #region Commands

        public Command ShowClientListCommand { get; set; }

        #endregion

        public ClientListingPageModel()
        {
            SelectedClient = new Client();

            // Display the client listing 
            ShowClientListCommand = new Command(async () => await ShowClientListing());
            ShowClientListCommand.Execute(null);
        }

        private async Task ShowClientListing()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Clear out the client collection
                Clients = new ObservableRangeCollection<Client>();

                // Call the service 
                var items = await App.ClientService.GetItems();

                // Add it to the collection
                Clients.AddRange(items.OrderBy(x => x.ClientName));
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
