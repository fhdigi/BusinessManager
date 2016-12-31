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

        public BudgetClassification Classification { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

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
    }
}
