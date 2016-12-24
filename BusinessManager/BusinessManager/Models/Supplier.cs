using AppServiceHelpers.Models;
using Newtonsoft.Json;
using PropertyChanged;

namespace BusinessManager.Models
{
    [ImplementPropertyChanged]
    public class Supplier : EntityData
    {
        //public string Id { get; set; }

        [JsonProperty(PropertyName = "SupplierName")]
        public string SupplierName { get; set; }

        public override string ToString() => SupplierName;
    }
}
