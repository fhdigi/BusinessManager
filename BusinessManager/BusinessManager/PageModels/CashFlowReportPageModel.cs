using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class CashFlowReportPageModel : BasePageModel
    {
        #region Properties

        public ObservableRangeCollection<Budget> ExpenseListing { get; set; }
        public double ExpenseTotal { get; set; }

        #endregion

        #region Commands

        public Command GetExpenseListingCommand { get; set; }

        #endregion

        public CashFlowReportPageModel()
        {
            ExpenseListing = new ObservableRangeCollection<Budget>();
            GetExpenseListingCommand = new Command(async () => await GetExpenses());
            GetExpenseListingCommand.Execute(null);
        }

        #region Methods

        private async Task GetExpenses()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                ExpenseListing.AddRange(await Budget.ConvertToBudgetList());
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

        #endregion

    }
}
