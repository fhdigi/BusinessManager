using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager.Models;
using BusinessManager.ViewModels;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
   public class BillPageModel : BaseViewModel
    {
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public Supplier SelectedSupplier { get; set; }
        public bool IsBusy { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }

        public ICommand SaveBillCommand { get; set; }

        public BillPageModel()
        {
            // Estalish the commands 
            SaveBillCommand = new Command(SaveBill);

            // This will become the selected supplier 
            SelectedSupplier = null;

            // Default data
            TransactionDate = DateTime.Today;
            Amount = 0.0;

            // We are going to need Suppliers and Ledgers 
            //var client = AppServiceHelpers.EasyMobileServiceClient.Current;
            //Suppliers = new ConnectedObservableCollection<Supplier>(client.Table<Supplier>());

            // This will fill in the supplier dropdown 
            Task.Run(async () => { await ExecuteRefreshCommand(); }).Wait();
        }

        private void SaveBill()
        {
            // make sure we have a supplier
            if (SelectedSupplier != null)
            {
                Ledger ledgerItem = new Ledger
                {
                    AssociativeId = SelectedSupplier.Id,
                    Amount = Amount,
                    TransactionDate = TransactionDate,
                    TransactionType = 1,
                    Description = SelectedSupplier.SupplierName
                };

                // Save the item
                //var client = AppServiceHelpers.EasyMobileServiceClient.Current;
                //Task.Run(async () => { await client.Table<Ledger>().AddAsync(ledgerItem); }).Wait();
            }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //await Suppliers.Refresh();
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
    }
}
