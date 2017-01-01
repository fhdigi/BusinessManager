using System;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using Xamarin.Forms;
using System.Linq;

namespace BusinessManager.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private Supplier _selectedSupplier;
        private AddSupplierViewModel addSupplierViewModel;

        public ObservableRangeCollection<Supplier> Suppliers { get; set; }

        public Supplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                ProcPropertyChanged(ref _selectedSupplier, value);
                ShowEditSupplierViewCommand.Execute(null);
            }
        }

        #region Command Definitions

        public Command GetSuppliersCommand { get; set; }
        public Command ShowAddSupplierViewCommand { get; set; }
        public Command ShowEditSupplierViewCommand { get; set; }

        #endregion

        public SupplierViewModel()
        {
            // we will need this view model later 
            addSupplierViewModel = new AddSupplierViewModel();

            // This becomes the observable collection of suppliers 
            Suppliers = new ObservableRangeCollection<Supplier>();

            // this is the command to show the add/edit supplier screen
            ShowAddSupplierViewCommand = new Command(async () => await AddSupplier());
            ShowEditSupplierViewCommand = new Command(async () => await EditSupplier());

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

        private async Task AddSupplier()
        {
            addSupplierViewModel.CurrentSupplier = new Supplier();
            addSupplierViewModel.EditMode = false;
            await SimpleIoc.NavigationService.PushModalAsync(addSupplierViewModel);
        }

        private async Task EditSupplier()
        {
            if (_selectedSupplier != null)
            {
                addSupplierViewModel.CurrentSupplier = _selectedSupplier;
                addSupplierViewModel.EditMode = true;
                await SimpleIoc.NavigationService.PushModalAsync(addSupplierViewModel);
            }
        }

        #endregion

    }
}
