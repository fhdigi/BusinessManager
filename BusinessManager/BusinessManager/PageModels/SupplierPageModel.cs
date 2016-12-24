using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppServiceHelpers;
using AppServiceHelpers.Helpers;
using BusinessManager.Models;
using BusinessManager.Services;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class SupplierPageModel : BaseViewModel
    {
        private ConnectedObservableCollection<Supplier> _suppliers;
        public ConnectedObservableCollection<Supplier> Suppliers
        {
            get
            {
                return _suppliers;
            }
            set
            {
                _suppliers = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        #region Command Definitions

        private Command _saveSupplierCommand;

        public Command SaveSupplierCommand
        {
            get
            {
                return _saveSupplierCommand ??
                       (_saveSupplierCommand = new Command(async () => await ExecuteSaveSupplierCommand()));
            }
        }

        Command _refreshCommand;

        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteRefreshCommand()));
            }
        }

        #endregion

        public SupplierPageModel()
        {
            var client = EasyMobileServiceClient.Current;
            Suppliers = new ConnectedObservableCollection<Supplier>(client.Table<Supplier>());

            CurrentSupplier = new Supplier();

            ExecuteRefreshCommand();
        }

        #region Command Methods

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Suppliers.Refresh();
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
                    var client = EasyMobileServiceClient.Current;
                    await client.Table<Supplier>().AddAsync(CurrentSupplier);

                    // close the screen 
                    await CoreMethods.PopPageModel();
                }
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

        #endregion

    }
}
