using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using Xamarin.Forms;
using System.Linq;

namespace BusinessManager.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Supplier> Suppliers { get; set; }

        private Supplier _currentSupplier;
        public Supplier CurrentSupplier
        {
            get
            {
                return _currentSupplier;        
            }
            set
            {
                ProcPropertyChanged(ref _currentSupplier, value);
            }
        }

        #region Command Definitions

        public Command GetSuppliersCommand { get; set; }
        public Command SaveSupplierCommand { get; set; }

        #endregion

        public SupplierViewModel()
        {
            // This becomes the observable collection of suppliers 
            Suppliers = new ObservableRangeCollection<Supplier>();

            // create a new supplier 
            CurrentSupplier = new Supplier();

            // this is the command to save a new supplier
            SaveSupplierCommand = new Command(async () => await SaveSupplier());

            // establish the command to get the list of suppliers 
            GetSuppliersCommand = new Command(async () => await GetSuppliers(),() => !IsBusy);

            // Get the list
            GetSuppliersCommand.Execute(null);
        }

        #region Methods

        async Task GetSuppliers()
        {
            // if the process is already running, get out 
            if (IsBusy)  return;

            // now set the flag to busy 
            IsBusy = true;

            try
            {
                // clear the current colleciton 
                Suppliers.Clear();

                // connect to the service 
                var service = new AzureService<Supplier>();

                // retrieve the items 
                var items = await service.GetItems();

                // add it to the collection and display
                Suppliers.AddRange(items.OrderBy(x => x.SupplierName));
            }
            catch (Exception ex)
            {
                SimpleIoc.SimpleIoc.DisplayErrorMessage(this, ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

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
                    SimpleIoc.SimpleIoc.DisplayAlert(this, "Save Confirmation", "The supplier record has been saved", "OK", "");

                    // Close the screen
                    await SimpleIoc.NavigationService.PopAsync();
                }
            }
            catch (Exception ex)
            {
                SimpleIoc.SimpleIoc.DisplayErrorMessage(this, ex.Message);
            }
            finally
            {
                IsBusy = false;
                GetSuppliersCommand.Execute(null);
            }
        }

        #endregion

    }
}
