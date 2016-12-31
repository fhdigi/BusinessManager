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

        #region Command Definitions

        public Command GetSuppliersCommand { get; set; }
        public Command ShowAddSupplierViewCommand { get; set; }

        #endregion

        public SupplierViewModel()
        {
            // This becomes the observable collection of suppliers 
            Suppliers = new ObservableRangeCollection<Supplier>();

            // this is the command to show the add/edit supplier screen
            ShowAddSupplierViewCommand = new Command(async () => await SimpleIoc.NavigationService.PushAsync(new AddSupplierViewModel()));

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

        #endregion

    }
}
