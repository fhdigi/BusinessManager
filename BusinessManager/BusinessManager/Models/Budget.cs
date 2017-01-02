using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessManager.Models
{
    public class Budget 
    {
        public enum BudgetClassification
        {
            Asset = 0,
            Expense = 1,
            Income = 2
        }

        #region Properties

        public BudgetClassification Classification { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        #endregion

        #region Constructor

        public Budget()
        {
            Classification = BudgetClassification.Asset;
            Description = "";
            Amount = 0.0;
        }

        public Budget(BudgetClassification classification, string desc, double amt)
        {
            Classification = classification;
            Description = desc;
            Amount = amt;
        }

        #endregion

        public static async Task<List<Budget>> ConvertToBudgetList()
        {
            List<Budget> budgetListing = new List<Budget>();
            var expenses = await App.LedgerService.GetItems();
            foreach (var expense in expenses)
            {
                budgetListing.Add(new Budget(Budget.BudgetClassification.Expense, expense.Description, expense.Amount));
            }
            return budgetListing;
        }
    }
}
