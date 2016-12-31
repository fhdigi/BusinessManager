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

        private Supplier _currentSupplier;

        public Supplier CurrentSupplier
        {
            get { return _currentSupplier; }
            set { ProcPropertyChanged(ref _currentSupplier, value); }
        }

        #endregion

        #region Commands

        public Command SaveSupplierCommand { get; set; }

        #endregion

        public AddSupplierViewModel()
        {
            // create a new supplier 
            CurrentSupplier = new Supplier();

            // this is the command to save a new supplier
            SaveSupplierCommand = new Command(async () => await SaveSupplier());
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
                    await service.SaveItem(CurrentSupplier);

                    // Just tell the user the save has been accomplished
                    SimpleIoc.SimpleIoc.DisplayAlert(this, "Save Confirmation", "The supplier record has been saved",
                        "OK", "");
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
                await NavigationService.PopAsync();
            }
        }

        #endregion

    }
}
