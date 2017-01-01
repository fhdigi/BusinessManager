using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BusinessManager.Models;

namespace BusinessManager.PageModels
{
    public class BudgetListingPageModel
    {
        private ObservableCollection<Budget> BudgetInformation { get; set; }
        public ObservableCollection<Budget> AssetListing { get; set; }
        public ObservableCollection<Budget> ExpenseListing { get; set; }
        public ObservableCollection<Budget> IncomeListing { get; set; }

        public double AssetTotal { get; set; }
        public double ExpenseTotal { get; set; }
        public double IncomeTotal { get; set; }

        public string NetCashFlow
        {
            get
            {
                double grandTotal = AssetTotal + IncomeTotal - ExpenseTotal;
                return $"Cash Remaining : {grandTotal:f2}";
            }
        }

        public BudgetListingPageModel()
        {
            // Get the items 
            BudgetInformation = new ObservableCollection<Budget>();
            GenerateBudgetItems();

			/*
            // break into categories 
			AssetListing = BudgetInformation.Where(x => x.Classification == Budget.BudgetClassification.Asset).ToObservableCollection();
            AssetTotal = AssetListing.Sum(x => x.Amount);

            ExpenseListing = BudgetInformation.Where(x => x.Classification == Budget.BudgetClassification.Expense).ToObservableCollection();
            ExpenseTotal = ExpenseListing.Sum(x => x.Amount);

            IncomeListing = BudgetInformation.Where(x => x.Classification == Budget.BudgetClassification.Income).ToObservableCollection();
            IncomeTotal = IncomeListing.Sum(x => x.Amount);
			*/
        }

        private void GenerateBudgetItems()
        {
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Asset, "Checking", 4260.54));
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Asset, "Savings", 15.04));
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Asset, "Purchase", 177.38));

            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Expense, "Excellus", 469.90));
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Expense, "Postmaster", 35.00));
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Expense, "Verizon Wireless", 273.0));
            BudgetInformation.Add(new Budget(Budget.BudgetClassification.Expense, "Amex", 676.64));
        }
    }
}
