using AppServiceHelpers.Models;
using Newtonsoft.Json;
using PropertyChanged;

namespace BusinessManager.Models
{
    [ImplementPropertyChanged]
    public class Client : EntityData
    {
        [JsonProperty(PropertyName = "ClientName")]
        public string ClientName { get; set; }
    }
}
