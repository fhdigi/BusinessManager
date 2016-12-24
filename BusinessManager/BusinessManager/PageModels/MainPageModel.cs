using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppServiceHelpers;
using AppServiceHelpers.Helpers;
using BusinessManager.Models;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class MainPageModel : FreshBasePageModel
    {
        public static ConnectedObservableCollection<Supplier> Suppliers { get; set; }

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

            var client = EasyMobileServiceClient.Current;
            Suppliers = new ConnectedObservableCollection<Supplier>(client.Table<Supplier>());
            ExecuteRefreshCommand();
        }

        private void ShowProjectsPage()
        {
            CoreMethods.PushPageModel<ProjectPageModel>();
        }

        private void ShowEnterNewClientPage()
        {
            CoreMethods.PushPageModel<ClientPageModel>();
        }

        private void ShowSuppliersPage()
        {
            CoreMethods.PushPageModel<SupplierPageModel>();
        }

        private void EnterNewBillPage()
        {
            CoreMethods.PushPageModel<BillPageModel>();
        }

        private void ShowBudgetListing()
        {
            CoreMethods.PushPageModel<BudgetListingPageModel>();
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await MainPageModel.Suppliers.Refresh();
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
