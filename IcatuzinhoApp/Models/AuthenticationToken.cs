using PropertyChanged;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class AuthenticationToken : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        [Indexed]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        [Indexed]
        public string RefreshToken { get; set; }
    }
}

