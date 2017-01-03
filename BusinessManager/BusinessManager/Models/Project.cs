using Newtonsoft.Json;


namespace BusinessManager.Models
{
    public class Project : BaseAzureEntityData
    {
        [JsonProperty(PropertyName = "ProjectName")]
        public string ProjectName { get; set; }

        [JsonProperty(PropertyName = "Client")]
        public string Client { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "PurchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty(PropertyName = "PurchaseOrderAmount")]
        public string PurchaseOrderAmount { get; set; }
    }
}
