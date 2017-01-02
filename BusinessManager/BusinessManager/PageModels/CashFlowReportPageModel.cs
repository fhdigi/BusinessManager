using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Constants;
using BusinessManager.Models;
using BusinessManager.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class CashFlowReportPageModel : BasePageModel
    {
        public ObservableRangeCollection<Ledger> ExpenseListing { get; set; }
        public double ExpenseTotal { get; set; }

        public Command GetExpenseListingCommand { get; set; }

        public CashFlowReportPageModel()
        {
            ExpenseListing = new ObservableRangeCollection<Ledger>();
            GetExpenseListingCommand = new Command(async () => await GetExpenses());
            GetExpenseListingCommand.Execute(null);
        }

        private async Task GetExpenses()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var expenses = await App.LedgerService.GetItems();
                ExpenseListing.AddRange(expenses);
                ExpenseTotal = ExpenseListing.Sum(x => x.Amount);
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
