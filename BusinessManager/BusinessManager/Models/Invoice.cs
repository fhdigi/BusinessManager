using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Invoice
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "AssociatedProjectId")]
        public string AssociatedProjectId { get; set; }

        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }
    }
}
