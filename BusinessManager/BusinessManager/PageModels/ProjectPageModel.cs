using System;
using System.Threading.Tasks;
using AppServiceHelpers;
using BusinessManager.Models;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class ProjectPageModel : BaseViewModel
    {
        public string ProjectName { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string PurchaseOrderAmount { get; set; }

        #region Commands

        private Command _saveProjectCommand;

        public Command SaveProjectCommand
        {
            get
            {
                return _saveProjectCommand ??
                       (_saveProjectCommand = new Command(async () => await ExecuteSaveProjectCommand()));
            }
        }

        #endregion

        public ProjectPageModel()
        {
            
        }

        async Task ExecuteSaveProjectCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Create a new Project model 
                Project newProject = new Project
                {
                    ProjectName = ProjectName,
                    Client = Client,
                    Description = Description,
                    PurchaseOrderAmount = PurchaseOrderAmount,
                    PurchaseOrderNumber = PurchaseOrderNumber
                };

                // Save the supplier 
                var client = EasyMobileServiceClient.Current;
                await client.Table<Project>().AddAsync(newProject);
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
