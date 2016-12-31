using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager.Models;
using BusinessManager.Services;
using BusinessManager.SimpleIoc;
using BusinessManager.ViewModels;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class MainPageModel : BaseViewModel
    {
        public static ObservableCollection<Supplier> Suppliers { get; set; }

        public bool IsBusy { get; set; }
        public string InitMessage { get; set; }
        public ICommand ShowBudgetListingPageCommand { get; set; }
        public ICommand ShowEnterNewBillPageCommand { get; set; }
        public ICommand ShowSuppliersPageCommand { get; set; }
        public ICommand ShowEnterNewClientPageCommand { get; set; }
        public ICommand ShowProjectsPageCommand { get; set; }

        public MainPageModel()
        {
            ShowBudgetListingPageCommand = new Command(ShowBudgetListing);
            ShowEnterNewBillPageCommand = new Command(EnterNewBillPage);
            ShowSuppliersPageCommand = new Command(ShowSuppliersPage);
            ShowEnterNewClientPageCommand = new Command(ShowEnterNewClientPage);
            ShowProjectsPageCommand = new Command(ShowProjectsPage);

            ExecuteRefreshCommand();
        }

        private void ShowProjectsPage()
        {
            //CoreMethods.PushPageModel<ProjectPageModel>();
        }

        private void ShowEnterNewClientPage()
        {
            //CoreMethods.PushPageModel<ClientPageModel>();
        }

        private async void ShowSuppliersPage()
        {
            await NavigationService.PushAsync(new SupplierViewModel());
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
            }
            finally
            {
                IsBusy = false;
                InitMessage = "Application Initialized";
            }
        }
    }
}
