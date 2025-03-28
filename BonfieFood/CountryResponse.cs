using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonfieFood
{
    public class CountryResponse
    {
        public const string COUNTRIES_API_URL = "http://api.geonames.org/countryInfoJSON?username=bonfie_food";
        [JsonProperty("geonames")]
        public List<CountryElement> Geonames { get; set; }
    }

    public class CountryElement
    {
        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}