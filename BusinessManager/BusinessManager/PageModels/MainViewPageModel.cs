using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager.Models;
using BusinessManager.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class MainViewPageModel : FreshMvvm.FreshBasePageModel
    {
        public string InitMessage { get; set; }
        public bool IsBusy { get; set; }

        public Command ShowSuppliersViewCommand { get; set; }
        public ICommand ShowBudgetListingPageCommand { get; set; }
        public ICommand ShowEnterNewBillPageCommand { get; set; }
        public ICommand ShowEnterNewClientPageCommand { get; set; }
        public ICommand ShowProjectsPageCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public MainViewPageModel()
        {
            ShowBudgetListingPageCommand = new Command(ShowBudgetListing);
            ShowEnterNewBillPageCommand = new Command(EnterNewBillPage);
            ShowEnterNewClientPageCommand = new Command(ShowEnterNewClientPage);
            ShowProjectsPageCommand = new Command(ShowProjectsPage);

            ShowSuppliersViewCommand = new Command(async () => await CoreMethods.PushPageModel<SupplierListingPageModel>());

            // Set the refresh command 
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            RefreshCommand.Execute(null);
        }

        private void ShowProjectsPage()
        {
            //CoreMethods.PushPageModel<ProjectPageModel>();
        }

        private void ShowEnterNewClientPage()
        {
            //CoreMethods.PushPageModel<ClientPageModel>();
        }

        private void EnterNewBillPage()
        {
            //CoreMethods.PushPageModel<BillPageModel>();
        }

        private void ShowBudgetListing()
        {
            //CoreMethods.PushPageModel<BudgetListingPageModel>();
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var service = new AzureService<Supplier>();
                await service.GetItems();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                InitMessage = "Application Initialized";
            }
        }
    }
}
