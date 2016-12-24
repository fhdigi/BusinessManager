using AppServiceHelpers.Models;
using Newtonsoft.Json;
using PropertyChanged;

namespace BusinessManager.Models
{
    [ImplementPropertyChanged]
    public class Project : EntityData
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
