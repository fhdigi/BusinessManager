using BusinessManager.PageModels;
using BusinessManager.Pages;
using BusinessManager.SimpleIoc;
using BusinessManager.ViewModels;
using Xamarin.Forms;

namespace BusinessManager
{
    public class App : Application
    {
        public App()
        {
            RegisterPages();
            NavigationService.SetRoot(new MainPageModel());
        }

        void RegisterPages()
        {
            SimpleIoc.SimpleIoc.RegisterPage<MainPageModel, MainPage>();
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
