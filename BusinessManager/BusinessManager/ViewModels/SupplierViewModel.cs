using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using BusinessManager.ViewModels;
using Xamarin.Forms;

namespace BusinessManager.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private ObservableCollection<Supplier> _suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get
            {
                return _suppliers;
            }
            set
            {
                _suppliers = value;
                ProcPropertyChanged(ref _suppliers, value);
            } 
        }

        private Supplier _currentSupplier;
        public Supplier CurrentSupplier
        {
            get
            {
                return _currentSupplier;        
            }
            set
            {
                _currentSupplier = value;
                ProcPropertyChanged(ref _currentSupplier, value);
            }
        }

        #region Command Definitions

        public Command GetSuppliersCommand { get; set; }

        private Command _saveSupplierCommand;

        public Command SaveSupplierCommand
        {
            get
            {
                return _saveSupplierCommand ??
                       (_saveSupplierCommand = new Command(async () => await ExecuteSaveSupplierCommand()));
            }
        }

        #endregion

        public SupplierViewModel()
        {
            // This becomes the observable collection of suppliers 
            Suppliers = new ObservableCollection<Supplier>();

            // create a new supplier 
            CurrentSupplier = new Supplier();

            // establish the command to get the list of suppliers 
            GetSuppliersCommand = new Command(async () => await GetSuppliers(),() => !IsBusy);

            GetSuppliers();
        }

        #region Command Methods

        async Task GetSuppliers()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var service = new AzureService<Supplier>();
                var items = await service.GetItems();

                Suppliers.Clear();
                foreach (var item in items)
                    Suppliers.Add(item);
            }
            catch (Exception ex)
            {
                //await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteSaveSupplierCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (CurrentSupplier != null)
                {
                    // Save the supplier 
                    //var client = EasyMobileServiceClient.Current;
                    //await client.Table<Supplier>().AddAsync(CurrentSupplier);

                    // close the screen 
                    //await CoreMethods.PopPageModel();
                }
            }
            catch (Exception ex)
            {
                //await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

    }
}
