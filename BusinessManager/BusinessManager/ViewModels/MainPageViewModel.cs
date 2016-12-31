using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager.Models;
using BusinessManager.Services;
using BusinessManager.SimpleIoc;
using Xamarin.Forms;

namespace BusinessManager.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _initMessage = "";
        public static ObservableCollection<Supplier> Suppliers { get; set; }

        public string InitMessage
        {
            get { return _initMessage; }
            set { ProcPropertyChanged(ref _initMessage, value); }
        }

        public Command ShowSuppliersViewCommand { get; set; }
        public ICommand ShowBudgetListingPageCommand { get; set; }
        public ICommand ShowEnterNewBillPageCommand { get; set; }
        public ICommand ShowEnterNewClientPageCommand { get; set; }
        public ICommand ShowProjectsPageCommand { get; set; }

        public MainPageViewModel()
        {
            ShowBudgetListingPageCommand = new Command(ShowBudgetListing);
            ShowEnterNewBillPageCommand = new Command(EnterNewBillPage);
            ShowEnterNewClientPageCommand = new Command(ShowEnterNewClientPage);
            ShowProjectsPageCommand = new Command(ShowProjectsPage);

            ShowSuppliersViewCommand = new Command(async () => await NavigationService.PushAsync(new SupplierViewModel()));

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
