using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Supplier : BaseAzureEntityData
    {
        [JsonProperty(PropertyName = "SupplierName")]
        public string SupplierName { get; set; }

        public override string ToString() => SupplierName;
    }
}
