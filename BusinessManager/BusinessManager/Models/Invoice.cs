using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Invoice : BaseAzureEntityData
    {
        [JsonProperty(PropertyName = "AssociatedProjectId")]
        public string AssociatedProjectId { get; set; }

        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }
    }
}
