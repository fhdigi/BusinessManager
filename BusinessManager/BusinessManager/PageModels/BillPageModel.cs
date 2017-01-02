using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessManager.Models;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class BillPageModel : BasePageModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public Supplier SelectedSupplier { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }

        public Command SaveBillCommand { get; set; }
        public Command FillSupplierListCommand { get; set; }
        public Command CancelBillCommand { get; set; }

        public BillPageModel()
        {
            // Estalish the command to save the bill
            SaveBillCommand = new Command(async () => await SaveBill());

            // When the user clicks the cancel button 
            CancelBillCommand = new Command(async () => await CoreMethods.PopPageModel(true));

            // Establish the command to fill in the supplier listing 
            FillSupplierListCommand = new Command(async () => await GetSupplierList());

            // This will become the selected supplier 
            SelectedSupplier = null;

            // Default data
            TransactionDate = DateTime.Today;
            Amount = 0.0;

            // This will fill in the supplier dropdown 
            FillSupplierListCommand.Execute(null);
        }

        private async Task SaveBill()
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

                // Save the bill
                await App.LedgerService.SaveItem(ledgerItem);

                // Close the screen
                await CoreMethods.PopPageModel();
            }
        }

        private async Task GetSupplierList()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Suppliers = await App.SupplierService.GetItems();
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
