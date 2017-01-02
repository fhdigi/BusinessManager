using Newtonsoft.Json;

namespace BusinessManager.Models
{
    public class Client 
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "ClientName")]
        public string ClientName { get; set; }
    }
}
