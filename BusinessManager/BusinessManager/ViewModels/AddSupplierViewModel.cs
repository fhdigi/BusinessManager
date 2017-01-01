using System;
using System.Threading.Tasks;
using BusinessManager.Models;
using Xamarin.Forms;
using BusinessManager.Services;
using BusinessManager.SimpleIoc;

namespace BusinessManager.ViewModels
{
    public class AddSupplierViewModel : BaseViewModel
    {
        #region Properties

        public bool EditMode;

        private Supplier _currentSupplier;

        public Supplier CurrentSupplier
        {
            get { return _currentSupplier; }
            set { ProcPropertyChanged(ref _currentSupplier, value); }
        }

        #endregion

        #region Commands

        public Command SaveSupplierCommand { get; set; }
        public Command CancelCommand { get; set; }

        #endregion

        public AddSupplierViewModel()
        {
            // this is the command to save a new supplier
            SaveSupplierCommand = new Command(async () => await SaveSupplier());

            // this gets us out 
            CancelCommand = new Command(async() => await NavigationService.PopModalAsync());
        }

        #region Methods

        async Task SaveSupplier()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (CurrentSupplier != null)
                {
                    // Save the supplier 
                    var service = new AzureService<Supplier>();

                    if (EditMode == false)
                    {
                        await service.SaveItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        SimpleIoc.SimpleIoc.DisplayAlert(this, "Save Confirmation", "The supplier record has been saved",
                            "OK", "");
                    }
                    else
                    {
                        await service.UpdateItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        SimpleIoc.SimpleIoc.DisplayAlert(this, "Update Confirmation", "The supplier record has been updated",
                            "OK", "");
                    }
                }
            }
            catch (Exception ex)
            {
                SimpleIoc.SimpleIoc.DisplayErrorMessage(this, ex.Message);
            }
            finally
            {
                IsBusy = false;

                // Close the screen
                await NavigationService.PopModalAsync();
            }
        }

        #endregion

    }
}
