using AppServiceHelpers;
using BusinessManager.Models;
using BusinessManager.PageModels;
using FreshMvvm;
using Xamarin.Forms;

namespace BusinessManager
{
    public class App : Application
    {
        public App()
        {
            // Set the load the app helpers 
            EasyMobileServiceClient.Current.Initialize("http://lccsexpense.azurewebsites.net/");
            EasyMobileServiceClient.Current.RegisterTable<Supplier>();
            EasyMobileServiceClient.Current.RegisterTable<Ledger>();
            EasyMobileServiceClient.Current.RegisterTable<Client>();
            EasyMobileServiceClient.Current.RegisterTable<Project>();
            EasyMobileServiceClient.Current.FinalizeSchema();

            // Navigation plumbing using FreshMvvm
            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
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
