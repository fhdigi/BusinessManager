using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Client 
    {
        [JsonProperty(PropertyName = "ClientName")]
        public string ClientName { get; set; }
    }
}
