using BusinessManager.SimpleIoc;
using BusinessManager.ViewModels;
using BusinessManager.Views;
using Xamarin.Forms;
using MainPageView = BusinessManager.Views.MainPageView;

namespace BusinessManager
{
    public class App : Application
    {
        public App()
        {
            RegisterPages();
            NavigationService.SetRoot(new MainPageViewModel());
        }

        void RegisterPages()
        {
            SimpleIoc.SimpleIoc.RegisterPage<MainPageViewModel, MainPageView>();
            SimpleIoc.SimpleIoc.RegisterPage<SupplierViewModel, SupplierView>();
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
