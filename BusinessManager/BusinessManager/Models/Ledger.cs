using System;
using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Ledger : BaseAzureEntityData
    {
        [JsonProperty(PropertyName = "AssociativeId")]
        public string AssociativeId { get; set; }

        [JsonProperty(PropertyName = "TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty(PropertyName = "TransactionType")]
        public int TransactionType { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "DebitAccount")]
        public int DebitAccount { get; set; }

        [JsonProperty(PropertyName = "CreditAccount")]
        public int CreditAccount { get; set; }

        [JsonProperty(PropertyName = "DateDue")]
        public DateTime DateDue { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "StatusLink")]
        public int StatusLink { get; set; }
        
        [JsonIgnore]
        public string FormattedTransactionDate => $"{Convert.ToDateTime(TransactionDate):MM/dd/yyyy}";

        [JsonIgnore]
        public string FormattedAmount => $"{Amount:c2}";
    }
}
