using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class MainViewPageModel : BasePageModel
    {
        public string InitMessage { get; set; }

        public ICommand ShowBudgetListingPageCommand { get; set; }
        public ICommand ShowProjectsPageCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command ShowCashFlowReportCommand { get; set; }

        public MainViewPageModel()
        {
            ShowBudgetListingPageCommand = new Command(ShowBudgetListing);
            ShowProjectsPageCommand = new Command(ShowProjectsPage);

            // When the user wants to see the cash flow report
            ShowCashFlowReportCommand = new Command(async () => await CoreMethods.PushPageModel<CashFlowReportPageModel>());

            // Set the refresh command 
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            RefreshCommand.Execute(null);
        }

        private void ShowProjectsPage()
        {
            //CoreMethods.PushPageModel<ProjectPageModel>();
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
                await App.SupplierService.GetItems();
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
