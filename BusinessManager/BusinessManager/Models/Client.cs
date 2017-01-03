using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Client : BaseAzureEntityData
    {
        [JsonProperty(PropertyName = "ClientName")]
        public string ClientName { get; set; }
    }
}
