using BusinessManager.Models;
using BusinessManager.PageModels;
using BusinessManager.Services;
using FreshMvvm;
using Xamarin.Forms;

namespace BusinessManager
{
    public class App : Application
    {
        public static AzureService<Supplier> SupplierService { get; set; }
        public static AzureService<Ledger> LedgerService { get; set; }
        public static AzureService<Client> ClientService { get; set; }

        public App()
        {
            // Establish connections to the services 
            SupplierService = new AzureService<Supplier>();
            LedgerService = new AzureService<Ledger>();
            ClientService = new AzureService<Client>();

            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Business Manager");
            masterDetailNav.AddPage<MainViewPageModel>("Home");
            masterDetailNav.AddPage<LedgerHomePageModel>("Ledger");
            masterDetailNav.AddPage<ArHomePageModel>("Receivables");
            masterDetailNav.AddPage<ApHomePageModel>("Payables");
            MainPage = masterDetailNav;
        }

        public bool IsBusy { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
