using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Supplier
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "SupplierName")]
        public string SupplierName { get; set; }

        public override string ToString() => SupplierName;
    }
}
