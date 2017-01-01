using BusinessManager.PageModels;
using FreshMvvm;
using Xamarin.Forms;

namespace BusinessManager
{
    public class App : Application
    {
        public App()
        {
            var page = FreshPageModelResolver.ResolvePageModel<MainViewPageModel>();
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
