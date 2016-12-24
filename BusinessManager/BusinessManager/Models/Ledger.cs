﻿using System;
using AppServiceHelpers.Models;
using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Ledger : EntityData
    {
        //public string Id { get; set; }

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

        [JsonIgnore]
        public string FormattedTransactionDate => string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(TransactionDate));

        [JsonIgnore]
        public string FormattedAmount => $"{Amount:c2}";
    }
}
