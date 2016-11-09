using System;
using PropertyChanged;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SQLite.Net.Attributes;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class AuthenticationToken : EntityBase
    { 
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName="token_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TokenType TokenType { get; set; }

        [JsonProperty(PropertyName="expires_in")]
        [Ignore]
        public int Expires { get; set; }

        [JsonIgnore]
        public TimeSpan ExpiresIn { get; private set; }

        public void SetExpirationTime()
        {
            ExpiresIn = TimeSpan.FromTicks(Expires);
        }

        public bool IsTokeExpired()
        {
            return DateTime.Now.Ticks < ExpiresIn.Ticks;
        }
    }
}

