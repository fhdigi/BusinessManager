using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class SupplierListingPageModel : BasePageModel
    {
        public ObservableRangeCollection<Supplier> Suppliers { get; set; }
        private Supplier _selectedSupplier;

        public Supplier SelectedSupplier
        {
            get
            {
                return _selectedSupplier;
            }
            set
            {
                _selectedSupplier = value;
                ShowEditSupplierViewCommand.Execute(null);
                _selectedSupplier = null;
            }
        }

        #region Command Definitions

        public Command GetSuppliersCommand { get; set; }
        public Command ShowAddSupplierViewCommand { get; set; }
        public Command ShowEditSupplierViewCommand { get; set; }

        #endregion

        public SupplierListingPageModel()
        {
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

        public override void ReverseInit(object returnedData)
        {
            if (returnedData != null)
            {
                GetSuppliersCommand.Execute(null);
            }
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

                // retrieve the items 
                var items = await App.SupplierService.GetItems();

                // add it to the collection and display
                Suppliers.AddRange(items.OrderBy(x => x.SupplierName));
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

        private async Task AddSupplier()
        {
            await CoreMethods.PushPageModel<AddSupplierPageModel>(null);
        }

        private async Task EditSupplier()
        {
            await CoreMethods.PushPageModel<AddSupplierPageModel>(_selectedSupplier);
        }

        #endregion

    }
}
